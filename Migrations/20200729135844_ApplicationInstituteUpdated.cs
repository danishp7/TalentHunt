using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ApplicationInstituteUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Institutes_InstituteId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_InstituteId",
                table: "Applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Applications_InstituteId",
                table: "Applications",
                column: "InstituteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Institutes_InstituteId",
                table: "Applications",
                column: "InstituteId",
                principalTable: "Institutes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
