namespace AuctionManagementSystem.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        // Note: Do not include sensitive information like PasswordHash in a DTO
    }
}