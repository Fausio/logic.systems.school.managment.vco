using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class prices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrollmentPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TuitionPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuitionPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentItemstPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnrollmentPriceId = table.Column<int>(type: "int", nullable: false),
                    EnrollmentPriceId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentItemstPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentItemstPrice_EnrollmentPrice_EnrollmentPriceId",
                        column: x => x.EnrollmentPriceId,
                        principalTable: "EnrollmentPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollmentItemstPrice_EnrollmentPrice_EnrollmentPriceId1",
                        column: x => x.EnrollmentPriceId1,
                        principalTable: "EnrollmentPrice",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentPriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentPriceeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentPriceHistory_EnrollmentPrice_EnrollmentPriceeId",
                        column: x => x.EnrollmentPriceeId,
                        principalTable: "EnrollmentPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TuitionPriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TuitionPriceId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Row = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedUSer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuitionPriceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TuitionPriceHistory_TuitionPrice_TuitionPriceId",
                        column: x => x.TuitionPriceId,
                        principalTable: "TuitionPrice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentItemstPrice_EnrollmentPriceId",
                table: "EnrollmentItemstPrice",
                column: "EnrollmentPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentItemstPrice_EnrollmentPriceId1",
                table: "EnrollmentItemstPrice",
                column: "EnrollmentPriceId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPriceHistory_EnrollmentPriceeId",
                table: "EnrollmentPriceHistory",
                column: "EnrollmentPriceeId");

            migrationBuilder.CreateIndex(
                name: "IX_TuitionPriceHistory_TuitionPriceId",
                table: "TuitionPriceHistory",
                column: "TuitionPriceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentItemstPrice");

            migrationBuilder.DropTable(
                name: "EnrollmentPriceHistory");

            migrationBuilder.DropTable(
                name: "TuitionPriceHistory");

            migrationBuilder.DropTable(
                name: "EnrollmentPrice");

            migrationBuilder.DropTable(
                name: "TuitionPrice");
        }
    }
}
