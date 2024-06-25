using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeartyBeat.Migrations
{
    public partial class CreateUserRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Obtained",
                table: "Reward");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Obtained",
                table: "Reward",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
