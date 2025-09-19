using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id); // FIX: Added '?'
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UpdateUser(UserDto userDto);
    }
}