using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eoss.Backend.Migrations
{
    /// <inheritdoc />
    public partial class Add_Devices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstallationParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Longitude = table.Column<double>(type: "double", nullable: false),
                    Latitude = table.Column<double>(type: "double", nullable: false),
                    Altitude = table.Column<double>(type: "double", nullable: false),
                    Roll = table.Column<double>(type: "double", nullable: false),
                    Pitch = table.Column<double>(type: "double", nullable: false),
                    Yaw = table.Column<double>(type: "double", nullable: false),
                    AngleToXAxis = table.Column<double>(type: "double", nullable: false),
                    AngleToYAxis = table.Column<double>(type: "double", nullable: false),
                    AngleToZAxis = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationParams", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PtzParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomePanToNorth = table.Column<double>(type: "double", nullable: false),
                    HomeTiltToHorizon = table.Column<double>(type: "double", nullable: false),
                    MinPanDegree = table.Column<float>(type: "float", nullable: false),
                    MaxPanDegree = table.Column<float>(type: "float", nullable: false),
                    MinTiltDegree = table.Column<float>(type: "float", nullable: false),
                    MaxTiltDegree = table.Column<float>(type: "float", nullable: false),
                    MinZoomLevel = table.Column<float>(type: "float", nullable: false),
                    MaxZoomLevel = table.Column<float>(type: "float", nullable: false),
                    FocalLength = table.Column<float>(type: "float", nullable: false),
                    MinPanNormal = table.Column<float>(type: "float", nullable: false),
                    MaxPanNormal = table.Column<float>(type: "float", nullable: false),
                    MinTiltNormal = table.Column<float>(type: "float", nullable: false),
                    MaxTiltNormal = table.Column<float>(type: "float", nullable: false),
                    MinZoomNormal = table.Column<float>(type: "float", nullable: false),
                    MaxZoomNormal = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtzParams", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ipv4Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Model = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Manufacturer = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Types = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Capabilities = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    InstallationParamsId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Devices_InstallationParams_InstallationParamsId",
                        column: x => x.InstallationParamsId,
                        principalTable: "InstallationParams",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Token = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoSourceToken = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoEncoding = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VideoWidth = table.Column<int>(type: "int", nullable: false),
                    VideoHeight = table.Column<int>(type: "int", nullable: false),
                    FrameRate = table.Column<float>(type: "float", nullable: false),
                    StreamUri = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AudioSourceToken = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AudioEncoding = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AudioBitrate = table.Column<int>(type: "int", nullable: false),
                    AudioSampleRate = table.Column<int>(type: "int", nullable: false),
                    PtzParamsId = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Profile_PtzParams_PtzParamsId",
                        column: x => x.PtzParamsId,
                        principalTable: "PtzParams",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_GroupId",
                table: "Devices",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_InstallationParamsId",
                table: "Devices",
                column: "InstallationParamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_DeviceId",
                table: "Profile",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_PtzParamsId",
                table: "Profile",
                column: "PtzParamsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "PtzParams");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "InstallationParams");
        }
    }
}
