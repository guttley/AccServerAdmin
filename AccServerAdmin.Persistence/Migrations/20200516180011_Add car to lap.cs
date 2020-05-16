using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class Addcartolap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CarId",
                table: "SessionLaps",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SessionLaps_CarId",
                table: "SessionLaps",
                column: "CarId");

            /*
            migrationBuilder.AddForeignKey(
                name: "FK_SessionLaps_SessionCars_CarId",
                table: "SessionLaps",
                column: "CarId",
                principalTable: "SessionCars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SessionLaps_SessionCars_CarId",
                table: "SessionLaps");

            migrationBuilder.DropIndex(
                name: "IX_SessionLaps_CarId",
                table: "SessionLaps");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "SessionLaps");
        }
    }
}
