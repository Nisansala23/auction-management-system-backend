using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services
{
    public interface IAuctionService
    {
        // Get a single auction by ID
        Task<Auction?> GetAuctionByIdAsync(int id);

        // Get all auctions
        Task<IEnumerable<Auction>> GetAllAuctionsAsync();

        // Create a new auction (linked to a user)
        Task<Auction> CreateAuctionAsync(CreateAuctionDto dto, int userId);

        // Update an existing auction
        Task<Auction?> UpdateAuctionAsync(int id, Auction updatedAuction);

        // Delete an auction
        Task<bool> DeleteAuctionAsync(int id);
    }
}