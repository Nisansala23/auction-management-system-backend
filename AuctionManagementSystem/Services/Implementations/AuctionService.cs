using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        // ---------------- Get Methods ----------------
        public async Task<Auction?> GetAuctionByIdAsync(int id)
        {
            return await _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.AuctionId == id);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsAsync()
        {
            return await _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .ToListAsync();
        }

        // ---------------- Create Auction ----------------
        public async Task<Auction> CreateAuctionAsync(CreateAuctionDto dto, int userId)
        {
            var auction = new Auction
            {
                Title = dto.Title,
                Description = dto.Description,
                StartPrice = dto.StartPrice,
                CurrentPrice = dto.StartPrice,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                UserId = userId
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return auction;
        }

        // ---------------- Update Auction ----------------
        public async Task<Auction?> UpdateAuctionAsync(int id, Auction updatedAuction)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null) return null;

            auction.Title = updatedAuction.Title;
            auction.Description = updatedAuction.Description;
            auction.StartPrice = updatedAuction.StartPrice;
            auction.CurrentPrice = updatedAuction.CurrentPrice;
            auction.StartTime = updatedAuction.StartTime;
            auction.EndTime = updatedAuction.EndTime;

            await _context.SaveChangesAsync();
            return auction;
        }

        // ---------------- Delete Auction ----------------
        public async Task<bool> DeleteAuctionAsync(int id)
        {
            var auction = await _context.Auctions.FindAsync(id);
            if (auction == null) return false;

            _context.Auctions.Remove(auction);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}