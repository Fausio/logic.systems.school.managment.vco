using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class pricesHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TuitionPriceId1",
                table: "TuitionPriceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentPriceId1",
                table: "EnrollmentPriceHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TuitionPriceHistory_TuitionPriceId1",
                table: "TuitionPriceHistory",
                column: "TuitionPriceId1");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentPriceHistory_EnrollmentPriceId1",
                table: "EnrollmentPriceHistory",
                column: "EnrollmentPriceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentPriceHistory_EnrollmentPrice_EnrollmentPriceId1",
                table: "EnrollmentPriceHistory",
                column: "EnrollmentPriceId1",
                principalTable: "EnrollmentPrice",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TuitionPriceHistory_TuitionPrice_TuitionPriceId1",
                table: "TuitionPriceHistory",
                column: "TuitionPriceId1",
                principalTable: "TuitionPrice",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentPriceHistory_EnrollmentPrice_EnrollmentPriceId1",
                table: "EnrollmentPriceHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TuitionPriceHistory_TuitionPrice_TuitionPriceId1",
                table: "TuitionPriceHistory");

            migrationBuilder.DropIndex(
                name: "IX_TuitionPriceHistory_TuitionPriceId1",
                table: "TuitionPriceHistory");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentPriceHistory_EnrollmentPriceId1",
                table: "EnrollmentPriceHistory");

            migrationBuilder.DropColumn(
                name: "TuitionPriceId1",
                table: "TuitionPriceHistory");

            migrationBuilder.DropColumn(
                name: "EnrollmentPriceId1",
                table: "EnrollmentPriceHistory");
        }
    }
}
