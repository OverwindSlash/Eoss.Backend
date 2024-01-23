using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eoss.Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNorth2East_PtzParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomePanToNorth",
                table: "PtzParams",
                newName: "HomePanToEast");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HomePanToEast",
                table: "PtzParams",
                newName: "HomePanToNorth");
        }
    }
}
