using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class AddDrivers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlayerId = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    Shortname = table.Column<string>(nullable: true),
                    DriverCategory = table.Column<int>(nullable: false),
                    HelmetTemplateKey = table.Column<int>(nullable: false),
                    HelmetBaseColor = table.Column<int>(nullable: false),
                    HelmetDetailColor = table.Column<int>(nullable: false),
                    HelmetMaterialType = table.Column<int>(nullable: false),
                    HelmetGlassColor = table.Column<int>(nullable: false),
                    HelmetGlassMetallic = table.Column<double>(nullable: false),
                    GlovesTemplateKey = table.Column<int>(nullable: false),
                    SuitTemplateKey = table.Column<int>(nullable: false),
                    SuitDetailColor1 = table.Column<int>(nullable: false),
                    SuitDetailColor2 = table.Column<int>(nullable: false),
                    AiSkill = table.Column<int>(nullable: false),
                    AiAggro = table.Column<int>(nullable: false),
                    AiRainSkill = table.Column<int>(nullable: false),
                    AiConsistency = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_PlayerId",
                table: "Drivers",
                column: "PlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
