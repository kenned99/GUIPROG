using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerSide.Migrations
{
    public partial class asdasdasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastKnownLocationId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GpsLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longtitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Accuracy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpsLocation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_LastKnownLocationId",
                table: "Members",
                column: "LastKnownLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_GpsLocation_LastKnownLocationId",
                table: "Members",
                column: "LastKnownLocationId",
                principalTable: "GpsLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_GpsLocation_LastKnownLocationId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "GpsLocation");

            migrationBuilder.DropIndex(
                name: "IX_Members_LastKnownLocationId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "LastKnownLocationId",
                table: "Members");
        }
    }
}
