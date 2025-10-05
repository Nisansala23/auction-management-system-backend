using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using AuctionManagementSystem.Hubs;

namespace AuctionManagementSystem.Services.Implementations
{
    public class BidService : IBidService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<BidHub> _hubContext;

        public BidService(ApplicationDbContext context, IHubContext<BidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
                .ToListAsync();
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
                .ToListAsync();
        }

        // Updated method signature to accept userId from the controller
        public async Task<BidDto> PlaceBidAsync(PlaceBidDto dto, int userId)
        {
            // 1. Validate auction
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.AuctionId == dto.AuctionId);

            if (auction == null) throw new Exception("Auction not found.");
            if (DateTime.UtcNow > auction.EndTime) throw new Exception("Auction has ended.");
            if (dto.Amount <= auction.CurrentPrice || dto.Amount <= auction.StartPrice)
                throw new Exception("Bid must be higher than current price.");

            // 2. Validate user using the userId passed from the controller
            var user = await _context.Users.FindAsync(userId);
            if (user == null) throw new Exception("User not found.");

            // 3. Create bid
            var bid = new Bid
            {
                Amount = dto.Amount,
                AuctionId = auction.AuctionId,
                UserId = userId, // <-- Use the userId from the parameter, not the DTO
                BidTime = DateTime.UtcNow
            };

            auction.CurrentPrice = dto.Amount;

            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();

            // 4. Prepare DTO return
            var bidDto = new BidDto
            {
                BidId = bid.BidId,
                BidAmount = bid.Amount,
                BidTime = bid.BidTime,
                Username = user.Username,
                AuctionTitle = auction.Title
            };

            // 5. Broadcast via SignalR to the auction group
            await _hubContext.Clients.Group($"auction-{auction.AuctionId}")
                .SendAsync("ReceiveBidUpdate", bidDto);

            return bidDto;
        }
    }
}