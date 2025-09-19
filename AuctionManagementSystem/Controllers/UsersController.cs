using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Models;
using AuctionManagementSystem.Services.Interfaces;

namespace AuctionManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("UsersController is working ✅");
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            var createdUser = _userService.Register(user);
            return Ok(createdUser);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _userService.UpdateUser(id, updatedUser);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            if (!result)
                return NotFound(new { message = "User not found" });

            return NoContent();
        }
    }
}
