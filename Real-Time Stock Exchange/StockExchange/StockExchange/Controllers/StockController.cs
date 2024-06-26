﻿
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockServiceLayer;

namespace StockExchange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly YahooApi _yahooApi;

        public StockController(YahooApi yahooApi)
        {
            _yahooApi = yahooApi;
        }

        [HttpGet("getAllStocks")]
        public async Task<IActionResult> GetSockets([FromQuery] List<string> StocksName)
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            //RecurringJob.AddOrUpdate(() =>
            //_yahooApi.GetAllStocks(StocksName), "*/50 * * * *");
            var stockdata = await _yahooApi.GetAllStocks(StocksName);   
            return Ok(stockdata);
        }

    }
}
