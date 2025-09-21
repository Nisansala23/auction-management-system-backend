using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        // GET: api/auctions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            var auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null) return NotFound();

            return Ok(auction);
        }

        // POST: api/auctions
        [HttpPost]
        public async Task<IActionResult> CreateAuction(CreateAuctionDto dto)
        {
            // In a real app, you'd get userId from auth claims
            int userId = dto.UserId;

            var auction = await _auctionService.CreateAuctionAsync(dto, userId);
            return CreatedAtAction(nameof(GetAuctionById), new { id = auction.AuctionId }, auction);
        }

        // PUT: api/auctions/{id}
        // This method has been updated to use AuctionDto as per your request.
        // It will only update the updatable properties.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuction(int id, AuctionDto updatedAuctionDto)
        {
            var existingAuction = await _auctionService.GetAuctionByIdAsync(id);
            if (existingAuction == null)
            {
                return NotFound();
            }

            // Map the DTO to the existing model, ignoring read-only properties like AuctionId
            existingAuction.Title = updatedAuctionDto.Title;
            existingAuction.Description = updatedAuctionDto.Description;
            existingAuction.StartPrice = updatedAuctionDto.StartPrice;
            existingAuction.EndTime = updatedAuctionDto.EndTime;

            var result = await _auctionService.UpdateAuctionAsync(id, existingAuction);
            return Ok(result);
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var success = await _auctionService.DeleteAuctionAsync(id);
            if (!success) return NotFound();

            return NoContent(); // 204 is conventional for delete
        }
    }
}
