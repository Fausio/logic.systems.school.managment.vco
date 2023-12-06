using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "row",
                table: "Tuition",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "row",
                table: "Student",
                newName: "Row");

            migrationBuilder.RenameColumn(
                name: "row",
                table: "SchoolLevel",
                newName: "Row");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3d4933be-7c51-408f-8b05-25bccb546f6f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffb935a3-5ee3-40c6-b8fe-412ad545467d", "AQAAAAEAACcQAAAAEBaAyw0m7GcrAZZeH1yL72+DtwKR+vuhO5MN8p8mSjjmzfgt18ovPYgt8LJT1lNeXQ==", "87ab4af4-0d65-47e0-b682-b275659ed962" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Tuition",
                newName: "row");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "Student",
                newName: "row");

            migrationBuilder.RenameColumn(
                name: "Row",
                table: "SchoolLevel",
                newName: "row");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "561a97cb-3ab3-4701-957b-c02b29506013");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b89ade60-2ed1-4ba9-8e05-7a34ec894976", "AQAAAAEAACcQAAAAEBDFbDltuso39kqEHxN9EzhmKEBRStimLuWO/pM07HQlUD+fEwiwIkly7sy5lwRKAg==", "290c4018-1be5-47f1-b289-4a470e6d03c7" });
        }
    }
}
