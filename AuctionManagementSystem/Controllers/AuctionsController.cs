using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/auctions
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            var auctions = _context.Auctions
                .Include(a => a.User)      // Include seller info
                .Include(a => a.Bids)      // Include bids
                .ToList();

            return Ok(auctions);
        }

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public IActionResult GetAuctionById(int id)
        {
            var auction = _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .FirstOrDefault(a => a.AuctionId == id);

            if (auction == null) return NotFound();
            return Ok(auction);
        }

        // POST: api/auctions
        [HttpPost]
        public IActionResult CreateAuction(CreateAuctionDto dto)
        {
            var auction = new Auction
            {
                Title = dto.Title,
                Description = dto.Description,
                StartPrice = dto.StartPrice,
                CurrentPrice = dto.StartPrice,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                UserId = dto.UserId
            };

            _context.Auctions.Add(auction);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionId }, auction);
        }

        // PUT: api/auctions/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAuction(int id, Auction updatedAuction)
        {
            var auction = _context.Auctions.Find(id);
            if (auction == null) return NotFound();

            auction.Title = updatedAuction.Title;
            auction.Description = updatedAuction.Description;
            auction.StartPrice = updatedAuction.StartPrice;
            auction.CurrentPrice = updatedAuction.CurrentPrice;
            auction.StartTime = updatedAuction.StartTime;
            auction.EndTime = updatedAuction.EndTime;

            _context.SaveChanges();

            return Ok(auction);
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAuction(int id)
        {
            var auction = _context.Auctions.Find(id);
            if (auction == null) return NotFound();

            _context.Auctions.Remove(auction);
            _context.SaveChanges();

            return Ok("Auction deleted");
        }
    }
}