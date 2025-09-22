using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Hubs
{
    public class BidHub : Hub
    {
        // Called by clients when they open an auction page
        public async Task JoinAuctionGroup(string auctionId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
        }

        // Optional: allow leaving group
        public async Task LeaveAuctionGroup(string auctionId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"auction-{auctionId}");
        }
    }
}