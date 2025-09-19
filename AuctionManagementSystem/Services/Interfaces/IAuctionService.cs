using AuctionManagementSystem.Models;
using AuctionManagementSystem.Dtos;
using System.Collections.Generic;

namespace AuctionManagementSystem.Services
{
    public interface IAuctionService
    {
        // Get all auctions
        IEnumerable<Auction> GetAllAuctions();

        // Get a single auction by ID
        Auction? GetAuctionById(int id);

        // Create a new auction
        Auction CreateAuction(CreateAuctionDto dto);

        // Update an existing auction
        Auction? UpdateAuction(int id, Auction updatedAuction);

        // Delete an auction
        bool DeleteAuction(int id);
    }
}
