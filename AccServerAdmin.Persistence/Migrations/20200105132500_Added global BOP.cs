using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class AddedglobalBOP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceOfPerformance",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Track = table.Column<string>(nullable: true),
                    Car = table.Column<int>(nullable: false),
                    Ballast = table.Column<int>(nullable: false),
                    Restrictor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceOfPerformance", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceOfPerformance");
        }
    }
}
