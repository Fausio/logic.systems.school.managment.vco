using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class ProfessorConfigRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClassLevel",
                table: "ProfessorConfig");

            migrationBuilder.DropColumn(
                name: "ClassRoom",
                table: "ProfessorConfig");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ProfessorConfig");

            migrationBuilder.AddColumn<int>(
                name: "ClassLevelId",
                table: "ProfessorConfig",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassRoomId",
                table: "ProfessorConfig",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ProfessorConfig",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorConfig_ClassLevelId",
                table: "ProfessorConfig",
                column: "ClassLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorConfig_ClassRoomId",
                table: "ProfessorConfig",
                column: "ClassRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorConfig_SubjectId",
                table: "ProfessorConfig",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_ClassLevelId",
                table: "ProfessorConfig",
                column: "ClassLevelId",
                principalTable: "SimpleEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_ClassRoomId",
                table: "ProfessorConfig",
                column: "ClassRoomId",
                principalTable: "SimpleEntity",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_SubjectId",
                table: "ProfessorConfig",
                column: "SubjectId",
                principalTable: "SimpleEntity",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_ClassLevelId",
                table: "ProfessorConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_ClassRoomId",
                table: "ProfessorConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorConfig_SimpleEntity_SubjectId",
                table: "ProfessorConfig");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorConfig_ClassLevelId",
                table: "ProfessorConfig");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorConfig_ClassRoomId",
                table: "ProfessorConfig");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorConfig_SubjectId",
                table: "ProfessorConfig");

            migrationBuilder.DropColumn(
                name: "ClassLevelId",
                table: "ProfessorConfig");

            migrationBuilder.DropColumn(
                name: "ClassRoomId",
                table: "ProfessorConfig");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ProfessorConfig");

            migrationBuilder.AddColumn<int>(
                name: "ClassLevel",
                table: "ProfessorConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClassRoom",
                table: "ProfessorConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Subject",
                table: "ProfessorConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
