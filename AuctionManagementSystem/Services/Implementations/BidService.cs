using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq; // Make sure this is included
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Implementations
{
    public class BidService : IBidService
    {
        private readonly ApplicationDbContext _context;

        public BidService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BidDto>> GetBidsByAuction(int auctionId)
        {
            return await _context.Bids
                .Where(b => b.AuctionId == auctionId)
                .Include(b => b.User)
                .Include(b => b.Auction)
                .OrderByDescending(b => b.BidTime)
                .Select(b => new BidDto
                {
                    BidId = b.BidId,
                    BidAmount = b.Amount,
                    BidTime = b.BidTime,
                    Username = b.User != null ? b.User.Username : null,
                    AuctionTitle = b.Auction != null ? b.Auction.Title : null
                })
                .ToListAsync(); // Use ToListAsync() for async operation
        }

        public async Task<IEnumerable<BidDto>> GetBidsByUser(int userId)
        {
            return await _context.Bids
                .Where(b => b.UserId == userId)
                .Include(b => b.User)
                .Include(b => b.Auction)
                .OrderByDescending(b => b.BidTime)
                .Select(b => new BidDto
                {
                    BidId = b.BidId,
                    BidAmount = b.Amount,
                    BidTime = b.BidTime,
                    Username = b.User != null ? b.User.Username : null,
                    AuctionTitle = b.Auction != null ? b.Auction.Title : null
                })
                .ToListAsync(); // Use ToListAsync() for async operation
        }
    }
}