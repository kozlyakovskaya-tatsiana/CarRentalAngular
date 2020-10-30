using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRental.DAL.Migrations.Application
{
    public partial class AddingCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mark = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Carcase = table.Column<int>(nullable: false),
                    ReleaseYear = table.Column<int>(nullable: false),
                    Transmission = table.Column<int>(nullable: false),
                    EnginePower = table.Column<double>(nullable: false),
                    FuelConsumption = table.Column<double>(nullable: false),
                    TankVolume = table.Column<double>(nullable: false),
                    FuelType = table.Column<int>(nullable: false),
                    TrunkVolume = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
