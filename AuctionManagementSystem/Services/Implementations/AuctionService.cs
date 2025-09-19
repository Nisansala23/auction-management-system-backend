using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Implementations
{
    public class AuctionService : IAuctionService
    {
        private readonly ApplicationDbContext _context;

        public AuctionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Auction?> GetAuctionById(int id)
        {
            return await _context.Auctions.FirstOrDefaultAsync(a => a.AuctionId == id);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return await _context.Auctions.ToListAsync();
        }

        // FIX: Add the 'int userId' parameter here
        public async Task<bool> CreateAuction(CreateAuctionDto auctionDto, int userId)
        {
            var auction = new Auction
            {
                Title = auctionDto.Title,
                Description = auctionDto.Description,
                // ... map other properties from the DTO
            };
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return false;
        }
    }
}