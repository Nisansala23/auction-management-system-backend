using AuctionManagementSystem.Data;
using AuctionManagementSystem.Dtos;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            // Find the user to update
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userDto.UserId);

            if (userToUpdate == null)
            {
                return false; // User not found
            }

            // Map the DTO to the model
            userToUpdate.Username = userDto.Username;
            userToUpdate.Email = userDto.Email;

            // Save the changes to the database asynchronously
            await _context.SaveChangesAsync();

            return true;
        }
    }
}