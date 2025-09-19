using AuctionManagementSystem.Models;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        User Register(User user);
        List<User> GetUsers();
        User? GetUserById(int id);
        User? UpdateUser(int id, User updatedUser);
        bool DeleteUser(int id);
    }
}