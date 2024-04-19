using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockDomainLayer.Models;
using StockServiceLayer.Contract;
using System.Security.Claims;

namespace StockExchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController1 : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHistory _history;

        public OrderController1(IOrderService orderService,IHistory history)
        {
            _orderService = orderService;
            _history = history;
        }
        [HttpGet("getAllOrders/{userId}")]
        public async Task<IActionResult> GetAllOrders(string userId)
        {
            if (userId!= User.FindFirstValue("userId"))
            {
                return Unauthorized();
            }
            var orders=await _orderService.GetAllOrders(userId);
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order=await _orderService.GetOrderById(id);
            return Ok(order);
        }
        [HttpPost("buyOrder")]
        public async Task<IActionResult> Buy(Order order)
        {
            order.UserId = User.FindFirstValue("userId");
            var newOrder=await _orderService.BuyOrder(order);
           
            await _history.Add(order,order.UserId);
            return Ok(newOrder);
        }
        [HttpPost("SellOrder{id}")]
        public async Task<IActionResult> SellOrder(int id,int quantity)
        {
            var result= await _orderService.SellOrder(id,quantity);
            if (result == null)
            {
                return BadRequest();
            }
            var order= await _orderService.GetOrderById(id);
            await _history.Add(order,order.UserId);
            return Ok(result);
        }
    }
}
