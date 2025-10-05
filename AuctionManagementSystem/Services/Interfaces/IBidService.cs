using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionManagementSystem.Dtos;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface IBidService
    {
        Task<IEnumerable<BidDto>> GetBidsByAuction(int auctionId);
        Task<IEnumerable<BidDto>> GetBidsByUser(int userId);

        Task<BidDto> PlaceBidAsync(PlaceBidDto dto, int userId); // <-- Updated
    }
}