using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LooseEndsApi.Migrations;

/// <inheritdoc />
public partial class Test : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "HasStarted",
            table: "GameSessions",
            newName: "IsStarted");

        migrationBuilder.AddColumn<int>(
            name: "Points",
            table: "Players",
            type: "INTEGER",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreatedUtc",
            table: "GameSessions",
            type: "TEXT",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<bool>(
            name: "InLobby",
            table: "GameSessions",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsCompleted",
            table: "GameSessions",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "DefaultResponses",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Content = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DefaultResponses", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Prompts",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Active = table.Column<bool>(type: "INTEGER", nullable: false),
                Content = table.Column<string>(type: "TEXT", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Prompts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Rounds",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                GameSessionId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rounds", x => x.Id);
                table.ForeignKey(
                    name: "FK_Rounds_GameSessions_GameSessionId",
                    column: x => x.GameSessionId,
                    principalTable: "GameSessions",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RoundPrompts",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RoundId = table.Column<int>(type: "INTEGER", nullable: false),
                PromptId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RoundPrompts", x => x.Id);
                table.ForeignKey(
                    name: "FK_RoundPrompts_Prompts_PromptId",
                    column: x => x.PromptId,
                    principalTable: "Prompts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RoundPrompts_Rounds_RoundId",
                    column: x => x.RoundId,
                    principalTable: "Rounds",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PlayerResponses",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                RoundPromptId = table.Column<int>(type: "INTEGER", nullable: false),
                Answer = table.Column<string>(type: "TEXT", nullable: true),
                Votes = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PlayerResponses", x => x.Id);
                table.ForeignKey(
                    name: "FK_PlayerResponses_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_PlayerResponses_RoundPrompts_RoundPromptId",
                    column: x => x.RoundPromptId,
                    principalTable: "RoundPrompts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_PlayerResponses_PlayerId",
            table: "PlayerResponses",
            column: "PlayerId");

        migrationBuilder.CreateIndex(
            name: "IX_PlayerResponses_RoundPromptId",
            table: "PlayerResponses",
            column: "RoundPromptId");

        migrationBuilder.CreateIndex(
            name: "IX_RoundPrompts_PromptId",
            table: "RoundPrompts",
            column: "PromptId");

        migrationBuilder.CreateIndex(
            name: "IX_RoundPrompts_RoundId",
            table: "RoundPrompts",
            column: "RoundId");

        migrationBuilder.CreateIndex(
            name: "IX_Rounds_GameSessionId",
            table: "Rounds",
            column: "GameSessionId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "DefaultResponses");

        migrationBuilder.DropTable(
            name: "PlayerResponses");

        migrationBuilder.DropTable(
            name: "RoundPrompts");

        migrationBuilder.DropTable(
            name: "Prompts");

        migrationBuilder.DropTable(
            name: "Rounds");

        migrationBuilder.DropColumn(
            name: "Points",
            table: "Players");

        migrationBuilder.DropColumn(
            name: "DateCreatedUtc",
            table: "GameSessions");

        migrationBuilder.DropColumn(
            name: "InLobby",
            table: "GameSessions");

        migrationBuilder.DropColumn(
            name: "IsCompleted",
            table: "GameSessions");

        migrationBuilder.RenameColumn(
            name: "IsStarted",
            table: "GameSessions",
            newName: "HasStarted");
    }
}
