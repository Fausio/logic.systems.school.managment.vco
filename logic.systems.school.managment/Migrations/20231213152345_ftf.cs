using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class ftf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolLevelId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_SchoolLevelId",
                table: "Enrollment",
                column: "SchoolLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_SimpleEntity_SchoolLevelId",
                table: "Enrollment",
                column: "SchoolLevelId",
                principalTable: "SimpleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_SimpleEntity_SchoolLevelId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_SchoolLevelId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "SchoolLevelId",
                table: "Enrollment");
        }
    }
}
