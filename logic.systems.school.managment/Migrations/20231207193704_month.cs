using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class month : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AddColumn<int>(
                name: "month",
                table: "Tuition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "year",
                table: "Tuition",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "month",
                table: "Tuition");

            migrationBuilder.DropColumn(
                name: "year",
                table: "Tuition");

            migrationBuilder.CreateIndex(
                name: "IX_Tuition_AssociatedLevelId",
                table: "Tuition",
                column: "AssociatedLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tuition_SimpleEntity_AssociatedLevelId",
                table: "Tuition",
                column: "AssociatedLevelId",
                principalTable: "SimpleEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
