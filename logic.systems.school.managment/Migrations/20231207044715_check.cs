using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "5a4222f8-9477-4895-a805-7ba07c266974");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b5a5790-44a3-47a6-8944-0e34d1c3e1f8", "AQAAAAEAACcQAAAAEP93jQvfsnRG2QnWc8SrHPUKf+ywkA0ky1ThZUiMXdxw5ih8EN3JApBGQYfDy/X5Hw==", "d2792949-3642-4b71-8f10-737f7e468ed0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
