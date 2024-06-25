using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeartyBeat.Migrations
{
    public partial class CreateAppUserFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Reward",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reward_AppUserId",
                table: "Reward",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_AspNetUsers_AppUserId",
                table: "Reward",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reward_AspNetUsers_AppUserId",
                table: "Reward");

            migrationBuilder.DropIndex(
                name: "IX_Reward_AppUserId",
                table: "Reward");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Reward");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
