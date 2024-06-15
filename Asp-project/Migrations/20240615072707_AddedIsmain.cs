using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp_project.Migrations
{
    public partial class AddedIsmain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "Banners");
        }
    }
}
