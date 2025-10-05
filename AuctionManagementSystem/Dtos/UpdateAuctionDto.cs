using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionManagementSystem.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating an existing auction.
    /// Only includes fields that a seller should be able to modify.
    /// </summary>
    public class UpdateAuctionDto
    {
        // Title and Description can be updated.
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        // The image URL can be updated.
        public string? ImageUrl { get; set; }

        // The end time can be extended, but must be in the future.
        [Required]
        public DateTime EndTime { get; set; }

        // Note: StartPrice and CurrentPrice are excluded as they should not be modified directly via this endpoint.
    }
}
