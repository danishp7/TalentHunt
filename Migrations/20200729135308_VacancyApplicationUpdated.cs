using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class VacancyApplicationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacancyId",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_VacancyId",
                table: "Applications",
                column: "VacancyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Vacancies_VacancyId",
                table: "Applications",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Vacancies_VacancyId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_VacancyId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "VacancyId",
                table: "Applications");
        }
    }
}
