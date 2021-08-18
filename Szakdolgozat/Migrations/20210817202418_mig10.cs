using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Szakdolgozat.Migrations
{
    public partial class mig10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnlineBusiness = table.Column<int>(type: "int", nullable: false),
                    Retail = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Catering",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesVolume = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catering", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberofEmployee = table.Column<int>(type: "int", nullable: false),
                    MaleEmployee = table.Column<int>(type: "int", nullable: false),
                    FemaleEmployee = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "GDP",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GDP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GDP", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StapleFoodPrice = table.Column<int>(type: "int", nullable: false),
                    PetrolPrice = table.Column<int>(type: "int", nullable: false),
                    GasOilPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Tourism",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TripForeignGuest = table.Column<int>(type: "int", nullable: false),
                    NightForeignGuest = table.Column<int>(type: "int", nullable: false),
                    SpendForeignGuest = table.Column<int>(type: "int", nullable: false),
                    TripHungarianGuest = table.Column<int>(type: "int", nullable: false),
                    NightHungarianGuest = table.Column<int>(type: "int", nullable: false),
                    SpendHungarianGuest = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tourism", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "Unemployed",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberofUnemployed = table.Column<int>(type: "int", nullable: false),
                    MaleUnemployed = table.Column<int>(type: "int", nullable: false),
                    FemaleUnemployed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unemployed", x => x.Date);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Catering");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "GDP");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Tourism");

            migrationBuilder.DropTable(
                name: "Unemployed");
        }
    }
}
