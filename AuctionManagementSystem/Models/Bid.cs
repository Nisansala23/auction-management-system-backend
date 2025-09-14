namespace AuctionManagementSystem.Models
{
    public class Bid
    {
        public int BidId { get; set; }
        public decimal Amount { get; set; }
        public DateTime BidTime { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User? User { get; set; }

        public int AuctionId { get; set; }
        public Auction? Auction { get; set; }
    }
}