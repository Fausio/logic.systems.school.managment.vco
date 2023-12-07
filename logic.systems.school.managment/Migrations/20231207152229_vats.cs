using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class vats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MonthlyFee",
                table: "Tuition",
                newName: "VatOfMonthlyFee");

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

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

           
            migrationBuilder.RenameColumn(
                name: "VatOfMonthlyFee",
                table: "Tuition",
                newName: "MonthlyFee");
        }
    }
}
