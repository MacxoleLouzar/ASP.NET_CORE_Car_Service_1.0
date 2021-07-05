using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarServiceApp.Data.Migrations
{
    public partial class AddServiceValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    miles = table.Column<double>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    dateAdded = table.Column<DateTime>(nullable: false),
                    servicetypeId = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_services_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_services_serviceTypes_servicetypeId",
                        column: x => x.servicetypeId,
                        principalTable: "serviceTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_services_CarId",
                table: "services",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_services_servicetypeId",
                table: "services",
                column: "servicetypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
