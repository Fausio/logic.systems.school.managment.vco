using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class EnrollmentItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "EnrollmentItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentItem_EnrollmentId",
                table: "EnrollmentItem",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentItem_Enrollment_EnrollmentId",
                table: "EnrollmentItem",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentItem_Enrollment_EnrollmentId",
                table: "EnrollmentItem");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentItem_EnrollmentId",
                table: "EnrollmentItem");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "EnrollmentItem");
        }
    }
}
