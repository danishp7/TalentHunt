using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ApplicationPropertiesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationAddress",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmployerStartDate",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "ApplicantAddress",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmploymentStartDate",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicantAddress",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmploymentStartDate",
                table: "Applications");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationAddress",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmployerStartDate",
                table: "Applications",
                type: "datetime2",
                nullable: true);
        }
    }
}
