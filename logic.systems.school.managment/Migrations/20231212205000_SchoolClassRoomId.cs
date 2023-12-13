using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class SchoolClassRoomId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolClassRoomId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SchoolClassRoomId",
                table: "Student",
                column: "SchoolClassRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_SimpleEntity_SchoolClassRoomId",
                table: "Student",
                column: "SchoolClassRoomId",
                principalTable: "SimpleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_SimpleEntity_SchoolClassRoomId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_SchoolClassRoomId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SchoolClassRoomId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "type",
                table: "Payment");
        }
    }
}
