using StockDomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockServiceLayer.Contract
{
    public interface IOrderService
    {
        Task<Order> AddOrder(Order order);
        Task<Order> BuyOrder(Order order);
        Task<string>  SellOrder(int id,int quantity);
        Task<Order>GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders(string userId);
    }
}
