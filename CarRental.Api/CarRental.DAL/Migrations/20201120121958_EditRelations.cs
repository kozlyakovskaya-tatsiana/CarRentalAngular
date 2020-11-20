using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations
{
    public partial class EditRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RentalPointId",
                table: "Locations",
                column: "RentalPointId",
                unique: true,
                filter: "[RentalPointId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_RentalPoints_RentalPointId",
                table: "Locations",
                column: "RentalPointId",
                principalTable: "RentalPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RentalPoints_LocationId",
                table: "RentalPoints",
                column: "LocationId",
                unique: true,
                filter: "[LocationId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_RentalPoints_Locations_LocationId",
                table: "RentalPoints",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
