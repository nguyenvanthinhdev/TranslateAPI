using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranslateAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTimeIP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfUsers = table.Column<int>(type: "int", nullable: true),
                    NumberOfUses = table.Column<int>(type: "int", nullable: true),
                    Active = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberIpSystem = table.Column<int>(type: "int", nullable: false),
                    NumberOfUsersSystem = table.Column<int>(type: "int", nullable: false),
                    NumberOfUsesSystem = table.Column<int>(type: "int", nullable: false),
                    NumberOfCoinSystem = table.Column<double>(type: "float", nullable: false),
                    NumberOfUsedCoin = table.Column<double>(type: "float", nullable: false),
                    NumberOfRemainingCoin = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreateTimeUser = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberOfuses = table.Column<int>(type: "int", nullable: true),
                    Coin = table.Column<double>(type: "float", nullable: false),
                    PypeUser = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translates",
                columns: table => new
                {
                    TranslateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    inpLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    outLanguage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslateCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeTranslates = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translates", x => x.TranslateID);
                    table.ForeignKey(
                        name: "FK_Translates_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Translates_UserID",
                table: "Translates",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Translates");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
