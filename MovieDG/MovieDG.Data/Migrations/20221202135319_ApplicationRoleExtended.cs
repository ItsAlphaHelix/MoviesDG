using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDG.Data.Migrations
{
    public partial class ApplicationRoleExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isActive",
                table: "AspNetRoles");
        }
    }
}
