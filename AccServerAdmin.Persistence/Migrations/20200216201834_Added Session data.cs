using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class AddedSessiondata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionDriver",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: true),
                    CarModel = table.Column<int>(nullable: false),
                    CupCategory = table.Column<long>(nullable: false),
                    RaceNumber = table.Column<long>(nullable: false),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDriver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionDriver_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServerName = table.Column<string>(nullable: false),
                    SessionTimestamp = table.Column<DateTime>(nullable: false),
                    SessionType = table.Column<string>(nullable: true),
                    Track = table.Column<string>(nullable: true),
                    IsWet = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionLap",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: true),
                    Laptime = table.Column<long>(nullable: false),
                    Split1 = table.Column<long>(nullable: false),
                    Split2 = table.Column<long>(nullable: false),
                    Split3 = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionLap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionLap_SessionDriver_DriverId",
                        column: x => x.DriverId,
                        principalTable: "SessionDriver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionLap_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionDriver_DriverId",
                table: "SessionDriver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLap_DriverId",
                table: "SessionLap",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLap_SessionId",
                table: "SessionLap",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionTimestamp",
                table: "Sessions",
                column: "SessionTimestamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Track",
                table: "Sessions",
                column: "Track");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionLap");

            migrationBuilder.DropTable(
                name: "SessionDriver");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
