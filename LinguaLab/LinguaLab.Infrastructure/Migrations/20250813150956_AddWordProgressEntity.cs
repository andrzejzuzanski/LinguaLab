using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinguaLab.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWordProgressEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_CreatedById",
                table: "Words");

            migrationBuilder.CreateTable(
                name: "WordProgresses",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EaseFactor = table.Column<double>(type: "float", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false),
                    Repetitions = table.Column<int>(type: "int", nullable: false),
                    NextReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordProgresses", x => new { x.UserId, x.WordId });
                    table.ForeignKey(
                        name: "FK_WordProgresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordProgresses_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordProgresses_WordId",
                table: "WordProgresses",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_CreatedById",
                table: "Words",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Words_Users_CreatedById",
                table: "Words");

            migrationBuilder.DropTable(
                name: "WordProgresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Words_Users_CreatedById",
                table: "Words",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
