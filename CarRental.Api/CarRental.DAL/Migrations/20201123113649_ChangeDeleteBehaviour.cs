using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class ChangeDeleteBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_RentalPoints_RentalPointId",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Locations_RentalPointId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "RentalPointId",
                table: "Locations");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "RentalPoints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalPoints_LocationId",
                table: "RentalPoints",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars",
                column: "RentalPointId",
                principalTable: "RentalPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RentalPoints_Locations_LocationId",
                table: "RentalPoints",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_RentalPoints_Locations_LocationId",
                table: "RentalPoints");

            migrationBuilder.DropIndex(
                name: "IX_RentalPoints_LocationId",
                table: "RentalPoints");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "RentalPoints");

            migrationBuilder.AddColumn<Guid>(
                name: "RentalPointId",
                table: "Locations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RentalPointId",
                table: "Locations",
                column: "RentalPointId",
                unique: true,
                filter: "[RentalPointId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_RentalPoints_RentalPointId",
                table: "Cars",
                column: "RentalPointId",
                principalTable: "RentalPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_RentalPoints_RentalPointId",
                table: "Locations",
                column: "RentalPointId",
                principalTable: "RentalPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
