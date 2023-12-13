using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class ft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "PaymentTuition",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "PaymentFine",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "PaymentEnrollment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "EnrollmentItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PaymentTuition");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PaymentFine");

            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PaymentEnrollment");

            migrationBuilder.AlterColumn<int>(
                name: "Description",
                table: "EnrollmentItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
