using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class Etype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "SimpleEntity",
                newName: "Type");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a1c75102-f1d3-4161-a4d7-f9e8e845bc21");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "80673d13-d577-4b07-bb36-aa27e91dfdc3", "AQAAAAEAACcQAAAAELuP+1xnicTgFL0bqAw8Ovuo5am/xoUGqoCGSfacJvcNiCVTfORQLKWwcGYbVRh8IA==", "cbd14837-2414-4e1b-9250-536ba643917c" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "SimpleEntity",
                newName: "type");

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
