using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface IAuctionService
    {
        Task<Auction?> GetAuctionById(int id); // FIX: Added '?'
        Task<IEnumerable<Auction>> GetAllAuctions();
        Task<bool> CreateAuction(CreateAuctionDto auctionDto, int userId);
    }
}