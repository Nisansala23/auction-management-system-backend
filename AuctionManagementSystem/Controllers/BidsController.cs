using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/bids/auction/5  → all bids for auction 5
        [HttpGet("auction/{auctionId}")]
        public IActionResult GetBidsForAuction(int auctionId)
        {
            var bids = _context.Bids
                .Include(b => b.User)
                .Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidTime)
                .ToList();

            return Ok(bids);
        }

        [HttpPost]
        public IActionResult PlaceBid(PlaceBidDto dto)
        {
            var auction = _context.Auctions.Find(dto.AuctionId);
            if (auction == null) return NotFound("Auction not found");

            if (dto.Amount <= auction.CurrentPrice)
            {
                return BadRequest("Bid must be higher than current price.");
            }

            var bid = new Bid
            {
                Amount = dto.Amount,
                BidTime = DateTime.UtcNow,
                UserId = dto.UserId,
                AuctionId = dto.AuctionId
            };

            auction.CurrentPrice = bid.Amount;

            _context.Bids.Add(bid);
            _context.SaveChanges();

            return Ok(bid);
        }
    }
}