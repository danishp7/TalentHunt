using Microsoft.EntityFrameworkCore.Migrations;

namespace TalentHunt.Migrations
{
    public partial class ExperienceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "Vacancies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Years = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Experience_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Experience_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "Vacancies");
        }
    }
}
