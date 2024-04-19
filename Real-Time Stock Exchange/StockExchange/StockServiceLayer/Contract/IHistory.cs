using StockDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServiceLayer.Contract
{
    public interface IHistory
    {
        Task<string> Add(Order newOrder, string userId);
        Task<History> getUserHistory(string userId);
    }
}
