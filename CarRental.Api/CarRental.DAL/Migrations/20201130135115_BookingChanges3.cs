using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class BookingChanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonSerialNumber",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "PersonPassportNumber",
                table: "Bookings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonPassportNumber",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "PersonPassportSerialNumber",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
