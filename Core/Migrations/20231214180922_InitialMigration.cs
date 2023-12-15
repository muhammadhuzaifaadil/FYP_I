using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UniversityCalendar",
                columns: table => new
                {
                    UCId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDeadline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventNotification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityCalendar", x => x.UCId);
                    table.ForeignKey(
                        name: "FK_UniversityCalendar_Universities_UId",
                        column: x => x.UId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversityDepartment",
                columns: table => new
                {
                    UDId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Campus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityDepartment", x => x.UDId);
                    table.ForeignKey(
                        name: "FK_UniversityDepartment_Universities_UId",
                        column: x => x.UId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversityDocument",
                columns: table => new
                {
                    UDocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentRequirement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityDocument", x => x.UDocId);
                    table.ForeignKey(
                        name: "FK_UniversityDocument_Universities_UId",
                        column: x => x.UId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversityFee",
                columns: table => new
                {
                    UFId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionFee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerCreditHourFee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityFee", x => x.UFId);
                    table.ForeignKey(
                        name: "FK_UniversityFee_Universities_UId",
                        column: x => x.UId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UniversityCalendar_UId",
                table: "UniversityCalendar",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityDepartment_UId",
                table: "UniversityDepartment",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityDocument_UId",
                table: "UniversityDocument",
                column: "UId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityFee_UId",
                table: "UniversityFee",
                column: "UId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniversityCalendar");

            migrationBuilder.DropTable(
                name: "UniversityDepartment");

            migrationBuilder.DropTable(
                name: "UniversityDocument");

            migrationBuilder.DropTable(
                name: "UniversityFee");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
