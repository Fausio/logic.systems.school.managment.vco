using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace logic.systems.school.managment.Migrations
{
    public partial class updattemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Student",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Student",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Student",
                newName: "MatherName");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SponsorId",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrgUnitProvince",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnitProvince", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sponsor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnitDistrict",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrgUnitProvinceId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnitDistrict", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgUnitDistrict_OrgUnitProvince_OrgUnitProvinceId",
                        column: x => x.OrgUnitProvinceId,
                        principalTable: "OrgUnitProvince",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactsType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SponsorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Sponsor_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "b0dcace5-a423-4d75-b85b-11ed27a2b232");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "169b9044-9f09-439f-979d-df5840a1e8b1", "AQAAAAEAACcQAAAAEG2rvQGdwE/Gkl5Dkce/MpcSbHeSxW95rrf6mU3bH/Sr8UDNSlnIyp2SLhFlT5xo0Q==", "9f8de642-474f-4ae7-b465-76084106eec8" });

            migrationBuilder.CreateIndex(
                name: "IX_Student_DistrictId",
                table: "Student",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_SponsorId",
                table: "Student",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SponsorId",
                table: "Contacts",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnitDistrict_OrgUnitProvinceId",
                table: "OrgUnitDistrict",
                column: "OrgUnitProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_OrgUnitDistrict_DistrictId",
                table: "Student",
                column: "DistrictId",
                principalTable: "OrgUnitDistrict",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Sponsor_SponsorId",
                table: "Student",
                column: "SponsorId",
                principalTable: "Sponsor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_OrgUnitDistrict_DistrictId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Sponsor_SponsorId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "OrgUnitDistrict");

            migrationBuilder.DropTable(
                name: "Sponsor");

            migrationBuilder.DropTable(
                name: "OrgUnitProvince");

            migrationBuilder.DropIndex(
                name: "IX_Student_DistrictId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_SponsorId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FatherName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "SponsorId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Student",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Student",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "MatherName",
                table: "Student",
                newName: "Email");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3d4933be-7c51-408f-8b05-25bccb546f6f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ffb935a3-5ee3-40c6-b8fe-412ad545467d", "AQAAAAEAACcQAAAAEBaAyw0m7GcrAZZeH1yL72+DtwKR+vuhO5MN8p8mSjjmzfgt18ovPYgt8LJT1lNeXQ==", "87ab4af4-0d65-47e0-b682-b275659ed962" });
        }
    }
}
