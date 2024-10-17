using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskService.Persistence.Migrations.EventStore
{
    /// <inheritdoc />
    public partial class InitialEventStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GlobalTaskProjection",
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
                    table.PrimaryKey("PK_GlobalTaskProjection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentToCheckTaskProjection",
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
                    table.PrimaryKey("PK_SentToCheckTaskProjection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskEvents",
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
                    table.PrimaryKey("PK_TaskEvents", x => new { x.Id, x.VersionId });
                });

            migrationBuilder.CreateTable(
                name: "TaskProjection",
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
                    table.PrimaryKey("PK_TaskProjection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlobalTaskProjection_Id",
                table: "GlobalTaskProjection",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SentToCheckTaskProjection_Id",
                table: "SentToCheckTaskProjection",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEvents_Id_VersionId",
                table: "TaskEvents",
                columns: new[] { "Id", "VersionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjection_Id",
                table: "TaskProjection",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlobalTaskProjection");

            migrationBuilder.DropTable(
                name: "SentToCheckTaskProjection");

            migrationBuilder.DropTable(
                name: "TaskEvents");

            migrationBuilder.DropTable(
                name: "TaskProjection");
        }
    }
}
