namespace AuctionManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }   // "Bidder" / "Seller" / "Admin"

        public ICollection<Auction>? Auctions { get; set; }
        public ICollection<Bid>? Bids { get; set; }
    }
}