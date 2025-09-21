using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionManagementSystem.Dtos
{
    public class AuctionDto
    {
        public int AuctionId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal StartPrice { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int UserId { get; set; }
    }
}
