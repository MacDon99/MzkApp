using Microsoft.EntityFrameworkCore.Migrations;

namespace MzkApp.Migrations
{
    public partial class addedStationIdToTimesOfArrivalsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods");

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "TimesOfArrivalsWithPeriods",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods");

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "TimesOfArrivalsWithPeriods",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
