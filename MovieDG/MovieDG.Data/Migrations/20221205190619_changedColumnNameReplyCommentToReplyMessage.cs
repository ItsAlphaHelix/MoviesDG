using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDG.Data.Migrations
{
    public partial class changedColumnNameReplyCommentToReplyMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplyComment",
                table: "AdminAnswers",
                newName: "ReplyMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplyMessage",
                table: "AdminAnswers",
                newName: "ReplyComment");
        }
    }
}
