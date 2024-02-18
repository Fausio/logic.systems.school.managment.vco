using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ProductInvoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoice_StudentId",
                table: "ProductInvoice",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoice_Student_StudentId",
                table: "ProductInvoice",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoice_Student_StudentId",
                table: "ProductInvoice");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoice_StudentId",
                table: "ProductInvoice");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ProductInvoice");
        }
    }
}
