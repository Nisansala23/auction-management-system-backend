using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        // GET: api/auctions
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            var auctions = _auctionService.GetAllAuctions();
            return Ok(auctions);
        }

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public IActionResult GetAuctionById(int id)
        {
            var auction = _auctionService.GetAuctionById(id);
            if (auction == null) return NotFound();

            return Ok(auction);
        }

        // POST: api/auctions
        [HttpPost]
        public IActionResult CreateAuction(CreateAuctionDto dto)
        {
            var auction = _auctionService.CreateAuction(dto);
            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionId }, auction);
        }

        // PUT: api/auctions/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAuction(int id, Auction updatedAuction)
        {
            var auction = _auctionService.UpdateAuction(id, updatedAuction);
            if (auction == null) return NotFound();

            return Ok(auction);
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAuction(int id)
        {
            var success = _auctionService.DeleteAuction(id);
            if (!success) return NotFound();

            return Ok("Auction deleted");
        }
    }
}
