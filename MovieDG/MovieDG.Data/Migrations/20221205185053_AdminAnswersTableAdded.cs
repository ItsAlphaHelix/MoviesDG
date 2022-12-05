﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesDG.Data.Migrations
{
    public partial class AdminAnswersTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminAnswers",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReplyComment = table.Column<string>(type: "nvarchar(700)", maxLength: 700, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminAnswers", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminAnswers_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminAnswers");
        }
    }
}
