using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportApp.Data.Migrations
{
    public partial class RouteInfoRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Routes_RouteId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "Schedules",
                newName: "RouteInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_RouteId",
                table: "Schedules",
                newName: "IX_Schedules_RouteInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Routes_RouteInfoId",
                table: "Schedules",
                column: "RouteInfoId",
                principalTable: "Routes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Routes_RouteInfoId",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "RouteInfoId",
                table: "Schedules",
                newName: "RouteId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedules_RouteInfoId",
                table: "Schedules",
                newName: "IX_Schedules_RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Routes_RouteId",
                table: "Schedules",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "Id");
        }
    }
}
