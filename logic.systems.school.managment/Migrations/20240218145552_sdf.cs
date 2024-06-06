using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class sdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductInvoiceId",
                table: "ProductPayment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPayment_ProductInvoiceId",
                table: "ProductPayment",
                column: "ProductInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPayment_ProductInvoice_ProductInvoiceId",
                table: "ProductPayment",
                column: "ProductInvoiceId",
                principalTable: "ProductInvoice",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPayment_ProductInvoice_ProductInvoiceId",
                table: "ProductPayment");

            migrationBuilder.DropIndex(
                name: "IX_ProductPayment_ProductInvoiceId",
                table: "ProductPayment");

            migrationBuilder.DropColumn(
                name: "ProductInvoiceId",
                table: "ProductPayment");
        }
    }
}
