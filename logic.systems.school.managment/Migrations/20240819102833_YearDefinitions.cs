using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class YearDefinitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearDefinitionId",
                table: "TuitionPrice",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearDefinitionId",
                table: "EnrollmentPrice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "YearDefinition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearDefinition", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TuitionPrice_YearDefinitionId",
                table: "TuitionPrice",
                column: "YearDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPrice_YearDefinitionId",
                table: "EnrollmentPrice",
                column: "YearDefinitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPrice_YearDefinition_YearDefinitionId",
                table: "EnrollmentPrice",
                column: "YearDefinitionId",
                principalTable: "YearDefinition",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TuitionPrice_YearDefinition_YearDefinitionId",
                table: "TuitionPrice",
                column: "YearDefinitionId",
                principalTable: "YearDefinition",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPrice_YearDefinition_YearDefinitionId",
                table: "EnrollmentPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_TuitionPrice_YearDefinition_YearDefinitionId",
                table: "TuitionPrice");

            migrationBuilder.DropTable(
                name: "YearDefinition");

            migrationBuilder.DropIndex(
                name: "IX_TuitionPrice_YearDefinitionId",
                table: "TuitionPrice");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentPrice_YearDefinitionId",
                table: "EnrollmentPrice");

            migrationBuilder.DropColumn(
                name: "YearDefinitionId",
                table: "TuitionPrice");

            migrationBuilder.DropColumn(
                name: "YearDefinitionId",
                table: "EnrollmentPrice");
        }
    }
}
