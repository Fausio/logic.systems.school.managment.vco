using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class Payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyFeeWithVat",
                table: "Tuition");

            migrationBuilder.DropColumn(
                name: "MonthlyFeeWithoutVat",
                table: "Tuition");

            migrationBuilder.DropColumn(
                name: "TuitionDate",
                table: "Tuition");

            migrationBuilder.DropColumn(
                name: "VatOfMonthlyFee",
                table: "Tuition");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TuitionId = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlyFeeWithoutVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VatOfMonthlyFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthlyFeeWithVat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Tuition_TuitionId",
                        column: x => x.TuitionId,
                        principalTable: "Tuition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_TuitionId",
                table: "Payment",
                column: "TuitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyFeeWithVat",
                table: "Tuition",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyFeeWithoutVat",
                table: "Tuition",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TuitionDate",
                table: "Tuition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "VatOfMonthlyFee",
                table: "Tuition",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
