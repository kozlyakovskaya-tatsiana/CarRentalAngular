using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class BookingInfoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonName = table.Column<string>(nullable: true),
                    PersonSurname = table.Column<string>(nullable: true),
                    PersonDateOfBirth = table.Column<DateTime>(nullable: false),
                    PersonPhoneNumber = table.Column<string>(nullable: true),
                    PersonPassportId = table.Column<string>(nullable: true),
                    PersonSerialNumber = table.Column<string>(nullable: true),
                    StartDateOfRenting = table.Column<DateTime>(nullable: false),
                    EndDateOfRenting = table.Column<DateTime>(nullable: false),
                    BookingStatus = table.Column<int>(nullable: false),
                    CarId = table.Column<Guid>(nullable: true),
                    UserId1 = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingInfo_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingInfo_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_CarId",
                table: "BookingInfo",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingInfo_UserId1",
                table: "BookingInfo",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingInfo");
        }
    }
}
