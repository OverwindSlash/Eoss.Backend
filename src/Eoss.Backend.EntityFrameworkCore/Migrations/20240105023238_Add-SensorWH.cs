using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eoss.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorWH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "SensorHeight",
                table: "PtzParams",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "SensorWidth",
                table: "PtzParams",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorHeight",
                table: "PtzParams");

            migrationBuilder.DropColumn(
                name: "SensorWidth",
                table: "PtzParams");
        }
    }
}
