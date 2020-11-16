using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class AddRentalPoint1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RentalPointId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RentalPoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPoints", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RentalPointId",
                table: "Cars",
                column: "RentalPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars",
                column: "RentalPointId",
                principalTable: "RentalPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "RentalPoints");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RentalPointId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RentalPointId",
                table: "Cars");
        }
    }
}
