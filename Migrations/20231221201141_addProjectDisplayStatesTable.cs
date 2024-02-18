using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskSched.Migrations
{
    /// <inheritdoc />
    public partial class addProjectDisplayStatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectDisplayStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SortDirection = table.Column<int>(type: "INTEGER", nullable: false),
                    SortParams = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDisplayStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectDisplayStates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDisplayStates_ProjectId",
                table: "ProjectDisplayStates",
                column: "ProjectId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectDisplayStates");
        }
    }
}
