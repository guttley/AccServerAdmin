using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class addedsimracerweatherandfixedconditionqually : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SimRacerWeatherConditions",
                table: "EventCfgs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isFixedConditionQualification",
                table: "EventCfgs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimRacerWeatherConditions",
                table: "EventCfgs");

            migrationBuilder.DropColumn(
                name: "isFixedConditionQualification",
                table: "EventCfgs");
        }
    }
}
