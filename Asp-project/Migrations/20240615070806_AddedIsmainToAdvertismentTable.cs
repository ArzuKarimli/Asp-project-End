using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asp_project.Migrations
{
    public partial class AddedIsmainToAdvertismentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "advertisments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "advertisments");
        }
    }
}
