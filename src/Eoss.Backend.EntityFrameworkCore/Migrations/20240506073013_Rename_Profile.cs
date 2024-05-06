using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eoss.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Rename_Profile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profile_Devices_DeviceId",
                table: "Profile");

            migrationBuilder.DropForeignKey(
                name: "FK_Profile_PtzParams_PtzParamsId",
                table: "Profile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profile",
                table: "Profile");

            migrationBuilder.RenameTable(
                name: "Profile",
                newName: "Profiles");

            migrationBuilder.RenameIndex(
                name: "IX_Profile_PtzParamsId",
                table: "Profiles",
                newName: "IX_Profiles_PtzParamsId");

            migrationBuilder.RenameIndex(
                name: "IX_Profile_DeviceId",
                table: "Profiles",
                newName: "IX_Profiles_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Devices_DeviceId",
                table: "Profiles",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_PtzParams_PtzParamsId",
                table: "Profiles",
                column: "PtzParamsId",
                principalTable: "PtzParams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Devices_DeviceId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_PtzParams_PtzParamsId",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "Profile");

            migrationBuilder.RenameIndex(
                name: "IX_Profiles_PtzParamsId",
                table: "Profile",
                newName: "IX_Profile_PtzParamsId");

            migrationBuilder.RenameIndex(
                name: "IX_Profiles_DeviceId",
                table: "Profile",
                newName: "IX_Profile_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profile",
                table: "Profile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_Devices_DeviceId",
                table: "Profile",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profile_PtzParams_PtzParamsId",
                table: "Profile",
                column: "PtzParamsId",
                principalTable: "PtzParams",
                principalColumn: "Id");
        }
    }
}
