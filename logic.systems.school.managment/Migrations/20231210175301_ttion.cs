using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class ttion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TuitionFines_TuitionId",
                table: "TuitionFines");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFines_TuitionId",
                table: "TuitionFines",
                column: "TuitionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TuitionFines_TuitionId",
                table: "TuitionFines");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionFines_TuitionId",
                table: "TuitionFines",
                column: "TuitionId");
        }
    }
}
