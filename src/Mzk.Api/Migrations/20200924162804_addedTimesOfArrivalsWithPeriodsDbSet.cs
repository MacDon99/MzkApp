using Microsoft.EntityFrameworkCore.Migrations;

namespace MzkApp.Migrations
{
    public partial class addedTimesOfArrivalsWithPeriodsDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesOfArrivals_Stations_StationId",
                table: "TimesOfArrivals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesOfArrivals",
                table: "TimesOfArrivals");

            migrationBuilder.RenameTable(
                name: "TimesOfArrivals",
                newName: "TimesOfArrivalsWithPeriods");

            migrationBuilder.RenameIndex(
                name: "IX_TimesOfArrivals_StationId",
                table: "TimesOfArrivalsWithPeriods",
                newName: "IX_TimesOfArrivalsWithPeriods_StationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesOfArrivalsWithPeriods",
                table: "TimesOfArrivalsWithPeriods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimesOfArrivalsWithPeriods_Stations_StationId",
                table: "TimesOfArrivalsWithPeriods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimesOfArrivalsWithPeriods",
                table: "TimesOfArrivalsWithPeriods");

            migrationBuilder.RenameTable(
                name: "TimesOfArrivalsWithPeriods",
                newName: "TimesOfArrivals");

            migrationBuilder.RenameIndex(
                name: "IX_TimesOfArrivalsWithPeriods_StationId",
                table: "TimesOfArrivals",
                newName: "IX_TimesOfArrivals_StationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimesOfArrivals",
                table: "TimesOfArrivals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TimesOfArrivals_Stations_StationId",
                table: "TimesOfArrivals",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
