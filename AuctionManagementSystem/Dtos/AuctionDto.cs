namespace AuctionManagementSystem.Dtos
{
    public class AuctionDto
    {
        public int AuctionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime EndTime { get; set; }

        // FIX: Add the missing properties
        public decimal StartPrice { get; set; }
        public DateTime StartTime { get; set; }
    }
}