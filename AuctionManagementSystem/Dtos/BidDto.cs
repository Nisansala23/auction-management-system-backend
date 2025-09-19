namespace AuctionManagementSystem.Dtos
{
	public class BidDto
	{
		public int BidId { get; set; }
		public decimal BidAmount { get; set; }
		public DateTime BidTime { get; set; }

		// Related details
		public string? Username { get; set; }       // from User table
		public string? AuctionTitle { get; set; }   // from Auction table
	}
}