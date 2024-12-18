using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoursesService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsOutOfDate = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    NameOfBlock = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseBlocks_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Task = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTasks_CourseBlocks_BlockId",
                        column: x => x.BlockId,
                        principalTable: "CourseBlocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseBlocks_CourseId",
                table: "CourseBlocks",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseBlocks_Id",
                table: "CourseBlocks",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Id",
                table: "Courses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseTasks_BlockId",
                table: "CourseTasks",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTasks_Id",
                table: "CourseTasks",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTasks");

            migrationBuilder.DropTable(
                name: "CourseBlocks");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
