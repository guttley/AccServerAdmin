using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowAutoDisqualification",
                table: "GameConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DumpEntryList",
                table: "GameConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RandomizeTrackWhenEmpty",
                table: "GameConfigurations",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShortFormationLap",
                table: "GameConfigurations",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowAutoDisqualification",
                table: "GameConfigurations");

            migrationBuilder.DropColumn(
                name: "DumpEntryList",
                table: "GameConfigurations");

            migrationBuilder.DropColumn(
                name: "RandomizeTrackWhenEmpty",
                table: "GameConfigurations");

            migrationBuilder.DropColumn(
                name: "ShortFormationLap",
                table: "GameConfigurations");
        }
    }
}
