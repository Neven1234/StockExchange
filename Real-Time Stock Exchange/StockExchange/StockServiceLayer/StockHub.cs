using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockDomainLayer.Models.Stock;
namespace StockServiceLayer
{
    public class StockHub:Hub
    {
        public async Task SendMessage(List<data> Stocksdata)
        {
            // Broadcasts the stock update to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", Stocksdata);
        }
    }
}
