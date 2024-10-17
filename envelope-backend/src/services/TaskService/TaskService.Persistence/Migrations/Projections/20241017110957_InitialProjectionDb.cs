using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskService.Persistence.Migrations.Projections
{
    /// <inheritdoc />
    public partial class InitialProjectionDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTaskEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VersionId = table.Column<int>(type: "integer", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Answer = table.Column<string>(type: "text", nullable: true),
                    Difficult = table.Column<int>(type: "integer", nullable: true),
                    ExecutionTime = table.Column<int>(type: "integer", nullable: true),
                    Author = table.Column<Guid>(type: "uuid", nullable: true),
                    BaseTaskUpdated_Name = table.Column<string>(type: "text", nullable: true),
                    BaseTaskUpdated_Description = table.Column<string>(type: "text", nullable: true),
                    BaseTaskUpdated_Answer = table.Column<string>(type: "text", nullable: true),
                    BaseTaskUpdated_Difficult = table.Column<int>(type: "integer", nullable: true),
                    BaseTaskUpdated_ExecutionTime = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTaskEvent", x => new { x.Id, x.VersionId });
                });

            migrationBuilder.CreateTable(
                name: "GlobalTaskProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    Difficult = table.Column<int>(type: "integer", nullable: false),
                    ExecutionTime = table.Column<int>(type: "integer", nullable: true),
                    Author = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalTaskProjections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentToCheckTaskProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    Difficult = table.Column<int>(type: "integer", nullable: false),
                    ExecutionTime = table.Column<int>(type: "integer", nullable: true),
                    Author = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentToCheckTaskProjections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskProjections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    Difficult = table.Column<int>(type: "integer", nullable: false),
                    ExecutionTime = table.Column<int>(type: "integer", nullable: true),
                    Author = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProjections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseTaskEvent_Id_VersionId",
                table: "BaseTaskEvent",
                columns: new[] { "Id", "VersionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GlobalTaskProjections_Id",
                table: "GlobalTaskProjections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentToCheckTaskProjections_Id",
                table: "SentToCheckTaskProjections",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjections_Id",
                table: "TaskProjections",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseTaskEvent");

            migrationBuilder.DropTable(
                name: "GlobalTaskProjections");

            migrationBuilder.DropTable(
                name: "SentToCheckTaskProjections");

            migrationBuilder.DropTable(
                name: "TaskProjections");
        }
    }
}
