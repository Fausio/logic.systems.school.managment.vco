using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfessorConfig",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorConfig_UserId",
                table: "ProfessorConfig",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorConfig_AspNetUsers_UserId",
                table: "ProfessorConfig",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorConfig_AspNetUsers_UserId",
                table: "ProfessorConfig");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorConfig_UserId",
                table: "ProfessorConfig");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProfessorConfig",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
