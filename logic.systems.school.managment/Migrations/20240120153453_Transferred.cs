using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class Transferred : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Transferred",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transferred",
                table: "Student");
        }
    }
}
