using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class any_tab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student");

            migrationBuilder.AlterColumn<string>(
                name: "Naturalness",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MatherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FatherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSchoolLevelId",
                table: "Student",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "de448f75-f51f-444d-ad33-d3eb6ccca9d5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef981d8-a320-4561-8b5a-905edc608027", "AQAAAAEAACcQAAAAEOoBfqjZjy0xEEMlIHiqdPl9aOCrVGYrvtQLrMILaV8sLh994iiSSjJFJB9RmKENxA==", "b1343dbb-5a4a-4732-ae6a-aacc65331c68" });

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId",
                principalTable: "SchoolLevel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student");

            migrationBuilder.AlterColumn<string>(
                name: "Naturalness",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MatherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FatherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentSchoolLevelId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a79da53f-20d6-4ed5-88a8-288ccbffcc69");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6302fb24-b8a5-4b6c-a25b-7adfa40b0574", "AQAAAAEAACcQAAAAEIc9lacvae5cbo8WpwlyRQiG2v0irlE6+EoZlNEeQcFFVE3Jy5jjezCalcLhUIkOuA==", "9dfa1c26-ca2e-4c1a-b2c8-7c8b9ad30d3c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SchoolLevel_CurrentSchoolLevelId",
                table: "Student",
                column: "CurrentSchoolLevelId",
                principalTable: "SchoolLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
