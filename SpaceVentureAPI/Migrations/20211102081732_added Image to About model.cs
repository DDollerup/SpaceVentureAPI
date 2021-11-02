using Microsoft.EntityFrameworkCore.Migrations;

namespace SpaceAdventureAPI.Migrations
{
    public partial class addedImagetoAboutmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Abouts",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Abouts");
        }
    }
}
