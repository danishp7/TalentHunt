using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ApplicationFormFieldsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicantContactNumber",
                table: "Applications",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Applications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AppUserId",
                table: "Applications",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId",
                table: "Applications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_AppUserId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_AppUserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Applications");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicantContactNumber",
                table: "Applications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
