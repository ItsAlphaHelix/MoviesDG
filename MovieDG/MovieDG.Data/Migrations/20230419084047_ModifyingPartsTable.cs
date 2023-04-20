using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDG.Data.Migrations
{
    public partial class ModifyingPartsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_PartId",
                table: "Movies",
                column: "PartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Parts_PartId",
                table: "Movies",
                column: "PartId",
                principalTable: "Parts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Parts_PartId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_PartId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PartId",
                table: "Movies");
        }
    }
}
