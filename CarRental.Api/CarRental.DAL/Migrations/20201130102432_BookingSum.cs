using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class BookingSum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfo_AspNetUsers_UserId1",
                table: "BookingInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookingInfo",
                table: "BookingInfo");

            migrationBuilder.RenameTable(
                name: "BookingInfo",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_BookingInfo_UserId1",
                table: "Bookings",
                newName: "IX_Bookings_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_BookingInfo_CarId",
                table: "Bookings",
                newName: "IX_Bookings_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId1",
                table: "Bookings",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cars_CarId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_UserId1",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "BookingInfo");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId1",
                table: "BookingInfo",
                newName: "IX_BookingInfo_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_CarId",
                table: "BookingInfo",
                newName: "IX_BookingInfo_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookingInfo",
                table: "BookingInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfo_AspNetUsers_UserId1",
                table: "BookingInfo",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
