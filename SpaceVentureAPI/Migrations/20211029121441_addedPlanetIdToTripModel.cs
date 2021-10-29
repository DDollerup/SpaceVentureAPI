using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceAdventureAPI.Migrations
{
    public partial class addedPlanetIdToTripModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Planet_PlanetId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "PlanetId",
                table: "Trips",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Planet_PlanetId",
                table: "Trips",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Planet_PlanetId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "PlanetId",
                table: "Trips",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Planet_PlanetId",
                table: "Trips",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
