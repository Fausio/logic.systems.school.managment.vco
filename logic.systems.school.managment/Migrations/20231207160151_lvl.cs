using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class lvl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tuition_SimpleEntity_AssociatedLevelId",
                table: "Tuition");

            migrationBuilder.DropIndex(
                name: "IX_Tuition_AssociatedLevelId",
                table: "Tuition");
        }
    }
}
