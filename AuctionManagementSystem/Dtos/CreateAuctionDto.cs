using Microsoft.AspNetCore.Http;
using System;

namespace AuctionManagementSystem.Dtos
{
    public class CreateAuctionDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }  // file upload
        public string? ImageUrl { get; set; }      // URL stored in DB

        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
