using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockServiceLayer.Contract;

namespace StockExchange.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistory _history;

        public HistoryController(IHistory history)
        {
            _history = history;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserHistory(string userId)
        {
            var userHistory=await _history.getUserHistory(userId);
            return Ok(userHistory);
        }
    }
}
