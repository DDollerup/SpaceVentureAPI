using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceAdventureAPI.Migrations
{
    public partial class addedTitleandContenttoBannermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Banners",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Banners",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Banners");
        }
    }
}
