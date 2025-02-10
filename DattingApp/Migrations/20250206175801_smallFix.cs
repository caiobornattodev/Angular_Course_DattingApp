using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DattingAppApi.Migrations
{
    /// <inheritdoc />
    public partial class smallFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Intruduction",
                table: "Users",
                newName: "Introduction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction",
                table: "Users",
                newName: "Intruduction");
        }
    }
}
