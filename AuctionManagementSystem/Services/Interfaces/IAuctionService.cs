using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface IAuctionService
    {
        // Get a single auction by ID
        // ?? UPDATED: Now returns AuctionDto? for consistency with DTO pattern.
        Task<AuctionDto?> GetAuctionByIdAsync(int id);

        // Get all auctions
        // ?? UPDATED: Now returns IEnumerable<AuctionDto> for consistency with DTO pattern.
        Task<IEnumerable<AuctionDto>> GetAllAuctionsAsync();

        // Create a new auction (linked to a user).
        // Return type changed to AuctionDto to return the created representation.
        Task<AuctionDto> CreateAuctionAsync(CreateAuctionDto dto, int userId);


        // Update an existing auction
        // ?? UPDATED: Accepts UpdateAuctionDto and returns the updated AuctionDto.
        Task<AuctionDto?> UpdateAuctionAsync(int id, UpdateAuctionDto updatedAuctionDto);

        // Delete an auction
        Task<bool> DeleteAuctionAsync(int id);
    }
}