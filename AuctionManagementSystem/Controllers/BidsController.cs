using AuctionManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/bids")]
    public class BidsController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        // Endpoint for "GET /api/bids/auction/{auctionId}"
        [HttpGet("auction/{auctionId}")]
        public async Task<IActionResult> GetBidsByAuction(int auctionId)
        {
            var bids = await _bidService.GetBidsByAuction(auctionId);
            if (bids == null)
            {
                return NotFound();
            }
            return Ok(bids);
        }

        // Endpoint for "GET /api/bids/user/{userId}"
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBidsByUser(int userId)
        {
            var bids = await _bidService.GetBidsByUser(userId);
            if (bids == null)
            {
                return NotFound();
            }
            return Ok(bids);
        }
    }
}