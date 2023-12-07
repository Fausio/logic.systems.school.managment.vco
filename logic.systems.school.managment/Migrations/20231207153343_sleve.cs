using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class sleve : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Tuition_SchoolLevel_AssociatedLevelId",
                table: "Tuition");

            migrationBuilder.DropIndex(
                name: "IX_Tuition_AssociatedLevelId",
                table: "Tuition");

            migrationBuilder.DropIndex(
                name: "IX_Student_CurrentSchoolLevelId",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Tuition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSchoolLevelId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tuition_StudentId",
                table: "Tuition",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuition_Student_StudentId",
                table: "Tuition",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuition_Student_StudentId",
                table: "Tuition");

            migrationBuilder.DropIndex(
                name: "IX_Tuition_StudentId",
                table: "Tuition");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Tuition");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSchoolLevelId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tuition_AssociatedLevelId",
                table: "Tuition",
                column: "AssociatedLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId",
                principalTable: "SchoolLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuition_SchoolLevel_AssociatedLevelId",
                table: "Tuition",
                column: "AssociatedLevelId",
                principalTable: "SchoolLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
