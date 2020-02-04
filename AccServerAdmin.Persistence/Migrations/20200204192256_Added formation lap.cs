using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class Addedformationlap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormationLap",
                table: "GameCfgs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormationLap",
                table: "GameCfgs");
        }
    }
}
