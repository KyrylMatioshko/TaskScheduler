using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskSched.Migrations
{
    /// <inheritdoc />
    public partial class CreateCompanyDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyDetails",
                columns: table => new
                {
                    CompanyDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDetails", x => x.CompanyDetailsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDetails_Name",
                table: "CompanyDetails",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyDetails");
        }
    }
}
