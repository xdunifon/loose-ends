using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LooseEndsApi.Migrations
{
    /// <inheritdoc />
    public partial class ModelUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundPrompts_Prompts_PromptId",
                table: "RoundPrompts");

            migrationBuilder.DropIndex(
                name: "IX_RoundPrompts_PromptId",
                table: "RoundPrompts");

            migrationBuilder.DropIndex(
                name: "IX_PlayerResponses_PlayerId",
                table: "PlayerResponses");

            migrationBuilder.DropColumn(
                name: "PromptId",
                table: "RoundPrompts");

            migrationBuilder.DropColumn(
                name: "Votes",
                table: "PlayerResponses");

            migrationBuilder.DropColumn(
                name: "InLobby",
                table: "GameSessions");

            migrationBuilder.RenameColumn(
                name: "IsStarted",
                table: "GameSessions",
                newName: "RoundTimer");

            migrationBuilder.AddColumn<bool>(
                name: "IsVoting",
                table: "Rounds",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Rounds",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "RoundPrompts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Prompt",
                table: "RoundPrompts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PlayerVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerVotes_PlayerResponses_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "PlayerResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerVotes_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerResponses_PlayerId",
                table: "PlayerResponses",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerVotes_PlayerId",
                table: "PlayerVotes",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerVotes_ResponseId",
                table: "PlayerVotes",
                column: "ResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerVotes");

            migrationBuilder.DropIndex(
                name: "IX_PlayerResponses_PlayerId",
                table: "PlayerResponses");

            migrationBuilder.DropColumn(
                name: "IsVoting",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "RoundPrompts");

            migrationBuilder.DropColumn(
                name: "Prompt",
                table: "RoundPrompts");

            migrationBuilder.RenameColumn(
                name: "RoundTimer",
                table: "GameSessions",
                newName: "IsStarted");

            migrationBuilder.AddColumn<int>(
                name: "PromptId",
                table: "RoundPrompts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "PlayerResponses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "InLobby",
                table: "GameSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RoundPrompts_PromptId",
                table: "RoundPrompts",
                column: "PromptId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerResponses_PlayerId",
                table: "PlayerResponses",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundPrompts_Prompts_PromptId",
                table: "RoundPrompts",
                column: "PromptId",
                principalTable: "Prompts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
