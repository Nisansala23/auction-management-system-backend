namespace AuctionManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        public ICollection<Auction>? Auctions { get; set; }
        public ICollection<Bid>? Bids { get; set; }
    }
}