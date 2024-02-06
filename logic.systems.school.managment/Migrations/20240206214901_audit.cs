using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          //  migrationBuilder.DropForeignKey(
          //      name: "FK_TuitionInvoice_Tuition_TuitionId",
          //      table: "TuitionInvoice");
          //
          //  migrationBuilder.DropIndex(
          //      name: "IX_TuitionInvoice_TuitionId",
          //      table: "TuitionInvoice");
          //
          //  migrationBuilder.DropColumn(
          //      name: "TuitionId",
          //      table: "TuitionInvoice");

            migrationBuilder.CreateTable(
                name: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit");

            migrationBuilder.AddColumn<int>(
                name: "TuitionId",
                table: "TuitionInvoice",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TuitionInvoice_TuitionId",
                table: "TuitionInvoice",
                column: "TuitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TuitionInvoice_Tuition_TuitionId",
                table: "TuitionInvoice",
                column: "TuitionId",
                principalTable: "Tuition",
                principalColumn: "Id");
        }
    }
}
