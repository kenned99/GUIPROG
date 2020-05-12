using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerSide.Migrations
{
    public partial class ads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_GpsLocation_LastKnownLocationId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GpsLocation",
                table: "GpsLocation");

            migrationBuilder.RenameTable(
                name: "GpsLocation",
                newName: "GpsLocations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GpsLocations",
                table: "GpsLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_GpsLocations_LastKnownLocationId",
                table: "Members",
                column: "LastKnownLocationId",
                principalTable: "GpsLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_GpsLocations_LastKnownLocationId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GpsLocations",
                table: "GpsLocations");

            migrationBuilder.RenameTable(
                name: "GpsLocations",
                newName: "GpsLocation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GpsLocation",
                table: "GpsLocation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_GpsLocation_LastKnownLocationId",
                table: "Members",
                column: "LastKnownLocationId",
                principalTable: "GpsLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
