// In AuctionManagementSystem.Dtos/AuctionDto.cs

using System;
using Microsoft.AspNetCore.Http; // Keep this if you use this DTO for creation as well

namespace AuctionManagementSystem.Dtos
{
    public class AuctionDto
    {
        // ?? FIX: Ensure these properties are present and match the types used in the ToDto method
        public int AuctionId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Accept uploaded file (only needed if this DTO is used for creation)
        public IFormFile? Image { get; set; }

        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; } // Must be present

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int UserId { get; set; } // Must be present
    }
}