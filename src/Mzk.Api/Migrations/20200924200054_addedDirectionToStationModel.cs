using Microsoft.EntityFrameworkCore.Migrations;

namespace MzkApp.Migrations
{
    public partial class addedDirectionToStationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "Stations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direction",
                table: "Stations");
        }
    }
}
