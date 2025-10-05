using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
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

        // GET: api/auctions (Returns IEnumerable<AuctionDto>)
        [HttpGet]
        public async Task<IActionResult> GetAllAuctions()
        {
            var auctions = await _auctionService.GetAllAuctionsAsync();
            return Ok(auctions);
        }

        // GET: api/auctions/{id} (Returns AuctionDto)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            // FIX: The service now returns AuctionDto, so the variable type is correct.
            var auction = await _auctionService.GetAuctionByIdAsync(id);
            if (auction == null) return NotFound();
            return Ok(auction);
        }

        // POST: api/auctions (Accepts CreateAuctionDto, Returns AuctionDto)
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateAuction([FromForm] CreateAuctionDto dto)
        {
            // 1. Get UserId securely
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized("Invalid or missing user ID in token.");
            }

            string? imageUrl = dto.ImageUrl; // Default to the provided URL (if any)

            // 2. Handle File Upload (ImageFile is on the DTO)
            if (dto.ImageFile != null)
            {
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                var filePath = Path.Combine(path, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.ImageFile.CopyToAsync(stream);
                }

                // Set the public URL (used by the DTO sent to the service)
                imageUrl = $"/images/{uniqueFileName}";
            }

            // 3. Update DTO and Call Service
            // This line ensures the service gets the final URL/path
            dto.ImageUrl = imageUrl;

            // FIX: The service now returns AuctionDto, so the variable type is correct.
            var auctionDto = await _auctionService.CreateAuctionAsync(dto, userId);

            return CreatedAtAction(nameof(GetAuctionById), new { id = auctionDto.AuctionId }, auctionDto);
        }

        // --- REFACTORED AND FIXED UPDATE ENDPOINT ---
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateAuction(int id, [FromBody] UpdateAuctionDto updatedAuctionDto)
        {
            // 1. Get the existing DTO for authorization check
            var existingAuctionDto = await _auctionService.GetAuctionByIdAsync(id);
            if (existingAuctionDto == null)
            {
                return NotFound();
            }

            // 2. Security Check (using properties from the DTO)
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (!int.TryParse(userIdString, out int currentUserId) ||
                (existingAuctionDto.UserId != currentUserId && userRole != "Admin"))
            {
                // User is neither the owner nor an Admin.
                return Forbid();
            }

            // 3. Call service with the ID and the DTO received from the client
            // FIX: This call now uses the correct signature: (int, UpdateAuctionDto)
            var resultDto = await _auctionService.UpdateAuctionAsync(id, updatedAuctionDto);

            // Check the result again, as the service might return null if update failed
            if (resultDto == null)
            {
                return NotFound();
            }

            return Ok(resultDto);
        }

        // DELETE: api/auctions/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var success = await _auctionService.DeleteAuctionAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }
    }
}