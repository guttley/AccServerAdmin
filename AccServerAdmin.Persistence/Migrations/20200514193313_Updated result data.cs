using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class Updatedresultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestLap",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BestSplit1",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BestSplit2",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BestSplit3",
                table: "Sessions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CollectResults",
                table: "Servers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SessionCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    CarModel = table.Column<int>(nullable: false),
                    CupCategory = table.Column<long>(nullable: false),
                    RaceNumber = table.Column<long>(nullable: false),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionLaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: true),
                    LapTime = table.Column<long>(nullable: false),
                    Split1 = table.Column<long>(nullable: false),
                    Split2 = table.Column<long>(nullable: false),
                    Split3 = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionLaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionLaps_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionLaps_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaderboardLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: true),
                    CurrentDriverId = table.Column<Guid>(nullable: true),
                    LastLap = table.Column<int>(nullable: false),
                    LastSplit1 = table.Column<int>(nullable: false),
                    LastSplit2 = table.Column<int>(nullable: false),
                    LastSplit3 = table.Column<int>(nullable: false),
                    BestLap = table.Column<int>(nullable: false),
                    BestSplit1 = table.Column<int>(nullable: false),
                    BestSplit2 = table.Column<int>(nullable: false),
                    BestSplit3 = table.Column<int>(nullable: false),
                    TotalTime = table.Column<int>(nullable: false),
                    LapCount = table.Column<int>(nullable: false),
                    MissingMandatoryPitstop = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaderboardLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_SessionCars_CarId",
                        column: x => x.CarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_Drivers_CurrentDriverId",
                        column: x => x.CurrentDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaderboardLines_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionCarDrivers",
                columns: table => new
                {
                    SessionCarId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCarDrivers", x => new { x.DriverId, x.SessionCarId });
                    table.ForeignKey(
                        name: "FK_SessionCarDrivers_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionCarDrivers_SessionCars_SessionCarId",
                        column: x => x.SessionCarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionPenalties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SessionId = table.Column<Guid>(nullable: false),
                    CarId = table.Column<Guid>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Penalty = table.Column<string>(nullable: true),
                    PenaltyValue = table.Column<int>(nullable: false),
                    ViolationInLap = table.Column<int>(nullable: false),
                    ClearedInLap = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionPenalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionPenalties_SessionCars_CarId",
                        column: x => x.CarId,
                        principalTable: "SessionCars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionPenalties_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_CarId",
                table: "LeaderboardLines",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_CurrentDriverId",
                table: "LeaderboardLines",
                column: "CurrentDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaderboardLines_SessionId",
                table: "LeaderboardLines",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionCarDrivers_SessionCarId",
                table: "SessionCarDrivers",
                column: "SessionCarId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_DriverId",
                table: "SessionLaps",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_SessionId",
                table: "SessionLaps",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPenalties_CarId",
                table: "SessionPenalties",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionPenalties_SessionId",
                table: "SessionPenalties",
                column: "SessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaderboardLines");

            migrationBuilder.DropTable(
                name: "SessionCarDrivers");

            migrationBuilder.DropTable(
                name: "SessionLaps");

            migrationBuilder.DropTable(
                name: "SessionPenalties");

            migrationBuilder.DropTable(
                name: "SessionCars");

            migrationBuilder.DropColumn(
                name: "BestLap",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSplit1",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSplit2",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "BestSplit3",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "CollectResults",
                table: "Servers");

            migrationBuilder.CreateTable(
                name: "SessionDriver",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CarModel = table.Column<int>(type: "INTEGER", nullable: false),
                    CupCategory = table.Column<long>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RaceNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    TeamName = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "SessionLap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DriverId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Laptime = table.Column<long>(type: "INTEGER", nullable: false),
                    SessionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Split1 = table.Column<long>(type: "INTEGER", nullable: false),
                    Split2 = table.Column<long>(type: "INTEGER", nullable: false),
                    Split3 = table.Column<long>(type: "INTEGER", nullable: false)
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
        }
    }
}
