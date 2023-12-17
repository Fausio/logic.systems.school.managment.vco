using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class EnrollmentInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "PaymentFine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EnrollmentInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentInvoice_Enrollment_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentFine_EnrollmentId",
                table: "PaymentFine",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentInvoice_EnrollmentId",
                table: "EnrollmentInvoice",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentFine_Enrollment_EnrollmentId",
                table: "PaymentFine",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentFine_Enrollment_EnrollmentId",
                table: "PaymentFine");

            migrationBuilder.DropTable(
                name: "EnrollmentInvoice");

            migrationBuilder.DropIndex(
                name: "IX_PaymentFine_EnrollmentId",
                table: "PaymentFine");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "PaymentFine");
        }
    }
}
