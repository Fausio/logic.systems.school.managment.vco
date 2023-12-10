using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class Naturalness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Naturalness",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Naturalness",
                table: "Student");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "a4331411-f504-4832-823e-5df15827f0bd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a964f646-f963-4542-876a-a4d3a988fb2e", "AQAAAAEAACcQAAAAEMQqY3u/tZYK797bK7MJ7NWF3J4weZjua+dBtknJlk/KsCeEpPGvxudgW8RxnWaNyQ==", "d5511716-0d44-4885-bcc0-6a80f8df83be" });
        }
    }
}
