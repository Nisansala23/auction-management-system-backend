using AuctionManagementSystem.Data;
using AuctionManagementSystem.Services.Interfaces;
using System.Threading.Tasks;
using AuctionManagementSystem.Models;

namespace AuctionManagementSystem.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendNotificationAsync(string message, int userId)
        {
            var notification = new Notification
            {
                Message = message,
                UserId = userId,
                CreatedAt = DateTime.UtcNow // FIX: Changed from SentTime to CreatedAt
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
        }
    }
}