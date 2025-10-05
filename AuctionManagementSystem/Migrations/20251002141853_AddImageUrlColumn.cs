using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementSystem.Migrations
{
    public partial class AddImageUrlColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add ImageUrl column to the existing Auctions table
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true); // nullable = not required
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the column if the migration is rolled back
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Auctions");
        }
    }
}
