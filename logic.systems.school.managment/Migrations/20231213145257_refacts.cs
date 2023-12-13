using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class refacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEnrollment_EnrollmentId",
                table: "PaymentEnrollment");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentEnrollmentId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnrollmentItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEnrollment_EnrollmentId",
                table: "PaymentEnrollment",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment");

            migrationBuilder.DropTable(
                name: "EnrollmentItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentEnrollment_EnrollmentId",
                table: "PaymentEnrollment");

            migrationBuilder.DropColumn(
                name: "PaymentEnrollmentId",
                table: "Enrollment");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Enrollment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentEnrollment_EnrollmentId",
                table: "PaymentEnrollment",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Student_StudentId",
                table: "Enrollment",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }
    }
}
