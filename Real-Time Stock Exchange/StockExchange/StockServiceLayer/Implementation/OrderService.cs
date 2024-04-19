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
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }
        public async Task<Order> AddOrder(Order order)
        {
            order.OrderType = "Buy";
            var OrderExist=await _repository.GetAsync(o=>o.SampleStockName==order.SampleStockName);
            if (OrderExist!=null) {
                await _repository.Update(order);
                return order;
            }
            var newOrder = await _repository.AddAsync(order);
            return newOrder;

        }

        public async Task<Order> BuyOrder(Order order)
        {
            order.OrderType = "Buy";
            var orderFromRepo = await _repository.GetAllAsync(o => o.SampleStockName == order.SampleStockName && o.UserId==order.UserId);
            if (orderFromRepo.Count()==0)
            {
                var newOrder = await _repository.AddAsync(order);
                return newOrder;
            }
            foreach (var item in orderFromRepo)
            {
                item.Quantity = item.Quantity + order.Quantity;
                await _repository.Update(item);
                
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrders(string userId)
        {
           return await _repository.GetAllAsync(o=>o.UserId == userId);

        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> SellOrder(int id,int quantity)
        {
            var soldOrders=await _repository.GetByIdAsync(id);
            if (soldOrders != null)
            {
                if (soldOrders.Quantity < quantity)
                {
                    return null;
                }
                if (soldOrders.Quantity == quantity)
                {
                    await _repository.Remove(id);
                    return $"you sold  all your {soldOrders.SampleStockName} Samples   Stock";
                }
                else
                {
                    soldOrders.Quantity=soldOrders.Quantity-quantity;
                    await _repository.Update(soldOrders);
                    return $"you sold  {quantity} from your {soldOrders.SampleStockName} Samples   Stock successfully";
                }
            }
            return null;
           
        }
    }
}
