using AuctionManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuctionManagementSystem.Dtos;

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
            return Ok(bids);
        }

        // Endpoint for "GET /api/bids/user/{userId}"
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBidsByUser(int userId)
        {
            var bids = await _bidService.GetBidsByUser(userId);
            return Ok(bids);
        }
        // New endpoint for placing bids
        [HttpPost("place")]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidDto dto)
        {
            try
            {
                var bid = await _bidService.PlaceBidAsync(dto);
                return Ok(bid);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        
        
    }
}