using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        // Helper method to convert an Auction Model to an AuctionDto
        private AuctionDto ToDto(Auction model)
        {
            // Calculate CurrentPrice: highest bid or start price
            var currentPrice = model.Bids != null && model.Bids.Any()
                             ? model.Bids.Max(b => b.Amount)
                             : model.StartPrice;

            return new AuctionDto
            {
                AuctionId = model.AuctionId,
                // 🛑 FIX: Use ?? string.Empty to prevent null reference warnings/errors
                Title = model.Title ?? string.Empty,
                Description = model.Description ?? string.Empty,
                ImageUrl = model.ImageUrl ?? string.Empty,
                StartPrice = model.StartPrice,
                CurrentPrice = currentPrice,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                UserId = model.UserId
            };
        }

        // ---------------- Get Methods ----------------

        // FIX: Method signature changed to return Task<AuctionDto?> and includes mapping
        public async Task<AuctionDto?> GetAuctionByIdAsync(int id)
        {
            var auction = await _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.AuctionId == id);

            if (auction == null) return null;

            return ToDto(auction);
        }

        // FIX: Method signature changed to return Task<IEnumerable<AuctionDto>> and includes mapping
        public async Task<IEnumerable<AuctionDto>> GetAllAuctionsAsync()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Bids)
                .ToListAsync();

            return auctions.Select(ToDto).ToList();
        }

        // ---------------- Create Auction ----------------

        // FIX: Method signature changed to return Task<AuctionDto> and includes mapping
        public async Task<AuctionDto> CreateAuctionAsync(CreateAuctionDto dto, int userId)
        {
            var auction = new Auction
            {
                Title = dto.Title,
                Description = dto.Description,
                // Assumes ImageUrl is correctly resolved in the Controller/DTO
                ImageUrl = dto.ImageUrl,
                StartPrice = dto.StartPrice,
                CurrentPrice = dto.StartPrice,
                StartTime = DateTime.UtcNow,
                EndTime = dto.EndTime,
                UserId = userId
            };

            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return ToDto(auction);
        }

        // ---------------- Update Auction ----------------

        // FIX: Method signature changed to accept UpdateAuctionDto and return Task<AuctionDto?>
        public async Task<AuctionDto?> UpdateAuctionAsync(int id, UpdateAuctionDto dto)
        {
            // Ensure Bids are included to accurately calculate the current price in ToDto
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.AuctionId == id);

            if (auction == null) return null;

            // Apply updates from the DTO
            if (dto.Title != null) auction.Title = dto.Title;
            if (dto.Description != null) auction.Description = dto.Description;
            if (dto.ImageUrl != null) auction.ImageUrl = dto.ImageUrl;

            // Assuming EndTime is mandatory and provided
            auction.EndTime = dto.EndTime;

            // Note: StartPrice/CurrentPrice are typically NOT updated here.

            await _context.SaveChangesAsync();

            return ToDto(auction);
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