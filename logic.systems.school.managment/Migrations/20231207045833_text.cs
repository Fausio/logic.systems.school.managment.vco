using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class text : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "adf7a262-a937-42c0-8566-d8009942735b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3f7eb3c-598e-4e53-9c94-a06696790d20", "AQAAAAEAACcQAAAAEEAA1/3gF9GRMVakD5/+yIf7MrnAxjK5dhhYTdDehWe/8L1sQ0dy6E3Vm/8I4f0meA==", "a6e31bf1-736f-4a82-94a2-96aa846f572b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
