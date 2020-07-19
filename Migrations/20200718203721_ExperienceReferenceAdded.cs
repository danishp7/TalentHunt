using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ExperienceReferenceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Users_UserId",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResponsibilities_Responsibilities_ResponsibilityId",
                table: "KeyResponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResponsibilities_Vacancies_VacancyId",
                table: "KeyResponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Experience_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experience",
                table: "Experience");

            migrationBuilder.RenameTable(
                name: "Experience",
                newName: "Experiences");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Users_UserId",
                table: "JobPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResponsibilities_Responsibilities_ResponsibilityId",
                table: "KeyResponsibilities",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResponsibilities_Vacancies_VacancyId",
                table: "KeyResponsibilities",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Experiences_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Users_UserId",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResponsibilities_Responsibilities_ResponsibilityId",
                table: "KeyResponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyResponsibilities_Vacancies_VacancyId",
                table: "KeyResponsibilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Experiences_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences");

            migrationBuilder.RenameTable(
                name: "Experiences",
                newName: "Experience");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experience",
                table: "Experience",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Users_UserId",
                table: "JobPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResponsibilities_Responsibilities_ResponsibilityId",
                table: "KeyResponsibilities",
                column: "ResponsibilityId",
                principalTable: "Responsibilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResponsibilities_Vacancies_VacancyId",
                table: "KeyResponsibilities",
                column: "VacancyId",
                principalTable: "Vacancies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Experience_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
