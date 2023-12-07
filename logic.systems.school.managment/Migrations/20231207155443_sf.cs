using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class sf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Student_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SimpleEntity_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId",
                principalTable: "SimpleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SimpleEntity_CurrentSchoolLevelId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CurrentSchoolLevelId",
                table: "Student");
        }
    }
}
