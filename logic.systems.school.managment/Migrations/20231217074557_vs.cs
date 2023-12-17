using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class vs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuition_Enrollment_EnrollmentId",
                table: "Tuition");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentId",
                table: "Tuition",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tuition_Enrollment_EnrollmentId",
                table: "Tuition",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuition_Enrollment_EnrollmentId",
                table: "Tuition");

            migrationBuilder.AlterColumn<int>(
                name: "EnrollmentId",
                table: "Tuition",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuition_Enrollment_EnrollmentId",
                table: "Tuition",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id");
        }
    }
}
