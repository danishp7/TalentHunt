using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ApplicationUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_EducationLevels_EducationLevelId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Employments_EmploymentId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EducationLevelId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_EmploymentId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "EducationLevels");

            migrationBuilder.AddColumn<string>(
                name: "MaximumEducationLevel",
                table: "EducationLevels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaximumEducationLevel",
                table: "EducationLevels");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "EducationLevels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EducationLevelId",
                table: "Applications",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmploymentId",
                table: "Applications",
                column: "EmploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_EducationLevels_EducationLevelId",
                table: "Applications",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Employments_EmploymentId",
                table: "Applications",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
