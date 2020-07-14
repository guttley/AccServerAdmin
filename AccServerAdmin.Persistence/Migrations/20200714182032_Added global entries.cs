using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccServerAdmin.Persistence.Migrations
{
    public partial class Addedglobalentries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GlobalEntryListId",
                table: "Entries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GlobalEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entries_GlobalEntryListId",
                table: "Entries",
                column: "GlobalEntryListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_GlobalEntries_GlobalEntryListId",
                table: "Entries");

            migrationBuilder.DropTable(
                name: "GlobalEntries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_GlobalEntryListId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "GlobalEntryListId",
                table: "Entries");
        }
    }
}
