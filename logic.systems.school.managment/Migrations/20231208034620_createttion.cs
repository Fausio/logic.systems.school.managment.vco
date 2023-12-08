using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class createttion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "year",
                table: "Tuition",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "monthName",
                table: "Tuition",
                newName: "MonthName");

            migrationBuilder.RenameColumn(
                name: "month",
                table: "Tuition",
                newName: "MonthNumber");

            migrationBuilder.AlterColumn<string>(
                name: "MonthName",
                table: "Tuition",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Tuition",
                newName: "year");

            migrationBuilder.RenameColumn(
                name: "MonthName",
                table: "Tuition",
                newName: "monthName");

            migrationBuilder.RenameColumn(
                name: "MonthNumber",
                table: "Tuition",
                newName: "month");

            migrationBuilder.AlterColumn<int>(
                name: "monthName",
                table: "Tuition",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
