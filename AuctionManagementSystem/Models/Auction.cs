namespace AuctionManagementSystem.Models
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal StartPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Bid>? Bids { get; set; }
    }
}