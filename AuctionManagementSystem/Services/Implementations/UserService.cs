using AuctionManagementSystem.Data;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;

namespace AuctionManagementSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Inside UserService class
        public User Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        // Get All Users
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        // Get User by Id
        public User? GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        // Update User
        public User? UpdateUser(int id, User updatedUser)
        {
            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
                return null;

            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            existingUser.Role = updatedUser.Role;

            _context.SaveChanges();
            return existingUser;
        }

        // Delete User
        public bool DeleteUser(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete == null)
                return false;

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            return true;
        }
    }
}