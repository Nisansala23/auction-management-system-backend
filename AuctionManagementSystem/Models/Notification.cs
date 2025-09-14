namespace AuctionManagementSystem.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }    // Primary Key
        public int UserId { get; set; }            // Foreign Key (points to who gets the notification)
        public User User { get; set; }             // Navigation

        public string Message { get; set; }        // Notification text (e.g., "You’ve been outbid!")
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When created
        public bool IsRead { get; set; } = false;  // Whether the user has read it
    }
}
