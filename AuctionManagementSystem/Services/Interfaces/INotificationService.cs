using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface INotificationService
    {
        // Uncomment this method to define the contract
        Task SendNotificationAsync(string message, int userId);
    }
}