using AuctionManagementSystem.Models;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AuctionManagementSystem.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly ApplicationDbContext _context;

        public AuctionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Auction> GetAllAuctions()
        {
            return _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .ToList();
        }

        public Auction? GetAuctionById(int id)
        {
            return _context.Auctions
                .Include(a => a.User)
                .Include(a => a.Bids)
                .FirstOrDefault(a => a.AuctionId == id);
        }

        public Auction CreateAuction(CreateAuctionDto dto)
        {
            var auction = new Auction
            {
                Title = dto.Title,
                Description = dto.Description,
                StartPrice = dto.StartPrice,
                CurrentPrice = dto.StartPrice,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                UserId = dto.UserId
            };

            _context.Auctions.Add(auction);
            _context.SaveChanges();

            return auction;
        }

        public Auction? UpdateAuction(int id, Auction updatedAuction)
        {
            var auction = _context.Auctions.Find(id);
            if (auction == null) return null;

            auction.Title = updatedAuction.Title;
            auction.Description = updatedAuction.Description;
            auction.StartPrice = updatedAuction.StartPrice;
            auction.CurrentPrice = updatedAuction.CurrentPrice;
            auction.StartTime = updatedAuction.StartTime;
            auction.EndTime = updatedAuction.EndTime;

            _context.SaveChanges();
            return auction;
        }

        public bool DeleteAuction(int id)
        {
            var auction = _context.Auctions.Find(id);
            if (auction == null) return false;

            _context.Auctions.Remove(auction);
            _context.SaveChanges();
            return true;
        }
    }
}
