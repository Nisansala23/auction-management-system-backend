using AuctionManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AuctionManagementSystem.Dtos;
using Microsoft.AspNetCore.Authorization; // <-- NEW
using System.Security.Claims; // <-- NEW
using System;

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
        [Authorize] // <-- NEW: Protect this endpoint
        public async Task<IActionResult> GetBidsByUser(int userId)
        {
            // NEW: Get the userId from the token to ensure a user can only view their own bids.
            var userIdFromTokenString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdFromTokenString, out int userIdFromToken) || userIdFromToken != userId)
            {
                return Forbid("You can only view your own bids.");
            }

            var bids = await _bidService.GetBidsByUser(userId);
            return Ok(bids);
        }

        // New endpoint for placing bids
        [HttpPost("place")]
        [Authorize] // <-- NEW: This endpoint requires a valid JWT to place a bid.
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidDto dto)
        {
            try
            {
                // NEW: Get the userId from the JWT claims.
                var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                {
                    return Unauthorized("Invalid or missing user ID in token.");
                }

                // Call the service with the userId from the token.
                var bid = await _bidService.PlaceBidAsync(dto, userId); // <-- Service method now needs userId
                return Ok(bid);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}