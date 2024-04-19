using StockDomainLayer.Models;
using StockRepositoryLayer.Data;
using StockServiceLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServiceLayer.Implementation
{
    public class HistoryService : IHistory
    {
        private readonly IRepository<History> _repository;

        public HistoryService(IRepository<History> repository)
        {
            _repository = repository;
        }
        public async Task<string> Add(Order newOrder, string userId)
        {
            var userHistoryExist = await _repository.GetAsync(h => h.userId == userId);
            if (userHistoryExist != null)
            {
                userHistoryExist.Order.Add(newOrder);
                _repository.Update(userHistoryExist);
                return "update history";
            }
            List<Order> orders = new List<Order> { newOrder };
            History history = new History
            {
                Order = orders,
                userId = userId
            };

            await _repository.AddAsync(history);
            return "add new history";
        }

        public async Task<History> getUserHistory(string userId)
        {
            return await _repository.GetAsync(h => h.userId == userId);
        }
    }
}
