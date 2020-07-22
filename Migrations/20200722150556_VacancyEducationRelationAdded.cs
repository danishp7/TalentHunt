using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class VacancyEducationRelationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "Vacancies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_EducationId",
                table: "Vacancies",
                column: "EducationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Educations_EducationId",
                table: "Vacancies",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Educations_EducationId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_EducationId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "Vacancies");
        }
    }
}
