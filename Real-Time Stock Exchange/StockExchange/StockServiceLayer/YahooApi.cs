using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using StockDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockDomainLayer.Models.Stock;

namespace StockServiceLayer
{
    public class YahooApi
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<StockHub> _hubContext;

        public YahooApi(IConfiguration configuration,IHubContext<StockHub> hubContext)
        {
            _configuration = configuration;
           _hubContext = hubContext;
        }
        public async Task< data> getStockJson(string SampleName)
        {
            string url = _configuration.GetSection("yahooAPI").GetValue<string>("url") + SampleName + "/ 1d / 15d";
            string key = _configuration.GetSection("yahooAPI").GetValue<string>("key");
            string host = _configuration.GetSection("yahooAPI").GetValue<string>("host");
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("X-RapidAPI-Key", key);
            request.AddHeader("X-RapidAPI-Host", host);
            RestResponse response = client.Execute(request);
            Stock.root value = JsonConvert.DeserializeObject<Stock.root>(response.Content);
            value.meta.timestamp = value.timestamp;
            
            Console.WriteLine(value.meta.regularMarketPrice+ " "+value.meta.symbol);
            return value.meta;
        }
        public async Task<List<data>> GetAllStocks(List<string> SamplesName) 
        {
            List<data> samplesData = new List<data>();
            foreach (string SampleName in SamplesName)
            {
              samplesData.Add(await getStockJson(SampleName));
            }
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", samplesData);
            return samplesData;
        }
    }
}
