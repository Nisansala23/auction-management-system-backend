namespace AuctionManagementSystem.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // plain text from client, will be hashed
        public string Role { get; set; }
    }
}
