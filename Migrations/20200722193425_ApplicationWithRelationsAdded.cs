using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ApplicationWithRelationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantName = table.Column<string>(nullable: true),
                    ApplicantEmail = table.Column<string>(nullable: true),
                    ApplicantContactNumber = table.Column<int>(nullable: false),
                    ApplicationAddress = table.Column<string>(nullable: true),
                    EducationLevelId = table.Column<int>(nullable: false),
                    DegreeStartDate = table.Column<DateTime>(nullable: false),
                    DegreeEndDate = table.Column<DateTime>(nullable: false),
                    EmploymentId = table.Column<int>(nullable: false),
                    EmployerStartDate = table.Column<DateTime>(nullable: false),
                    EmploymentEndDate = table.Column<DateTime>(nullable: false),
                    InstituteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Employments_EmploymentId",
                        column: x => x.EmploymentId,
                        principalTable: "Employments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Institutes_InstituteId",
                        column: x => x.InstituteId,
                        principalTable: "Institutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EducationLevelId",
                table: "Applications",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmploymentId",
                table: "Applications",
                column: "EmploymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_InstituteId",
                table: "Applications",
                column: "InstituteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");
        }
    }
}
