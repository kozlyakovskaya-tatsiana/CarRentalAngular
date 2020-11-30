using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class BookingCaRDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingInfo_Cars_CarId",
                table: "BookingInfo",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
