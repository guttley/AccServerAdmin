using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class AddedAssistRules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssistRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    StabilityControlLevelMax = table.Column<int>(nullable: false),
                    DisableAutosteer = table.Column<bool>(nullable: false),
                    DisableAutoLights = table.Column<bool>(nullable: false),
                    DisableAutoWiper = table.Column<bool>(nullable: false),
                    DisableAutoEngineStart = table.Column<bool>(nullable: false),
                    DisableAutoPitLimiter = table.Column<bool>(nullable: false),
                    DisableAutoGear = table.Column<bool>(nullable: false),
                    DisableAutoClutch = table.Column<bool>(nullable: false),
                    DisableIdealLine = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssistRules_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssistRules_ServerId",
                table: "AssistRules",
                column: "ServerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssistRules");
        }
    }
}
