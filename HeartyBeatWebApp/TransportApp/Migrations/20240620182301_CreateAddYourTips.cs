﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeartyBeat.Migrations
{
    public partial class CreateAddYourTips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HealthyTIpsPersonal");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "HealthyTIpsPersonal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "HealthyTIpsPersonal");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HealthyTIpsPersonal",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
