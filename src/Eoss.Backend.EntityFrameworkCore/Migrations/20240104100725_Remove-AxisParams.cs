using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eoss.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAxisParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Group_GroupId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Group",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "AngleToXAxis",
                table: "InstallationParams");

            migrationBuilder.DropColumn(
                name: "AngleToYAxis",
                table: "InstallationParams");

            migrationBuilder.DropColumn(
                name: "AngleToZAxis",
                table: "InstallationParams");

            migrationBuilder.RenameTable(
                name: "Group",
                newName: "Groups");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Groups_GroupId",
                table: "Devices",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Groups_GroupId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "Group");

            migrationBuilder.AddColumn<double>(
                name: "AngleToXAxis",
                table: "InstallationParams",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AngleToYAxis",
                table: "InstallationParams",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "AngleToZAxis",
                table: "InstallationParams",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Group",
                table: "Group",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Group_GroupId",
                table: "Devices",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id");
        }
    }
}
