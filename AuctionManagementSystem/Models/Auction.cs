namespace AuctionManagementSystem.Models
{
    public class Auction
    {
        public int AuctionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? ImageUrl { get; set; } // Optional (NULLABLE in SQL)
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