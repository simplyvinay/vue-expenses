using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vue_expenses_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseCateogries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCateogries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Hash = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCateogries_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCateogries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ExpenseCateogries",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), "", "General Expenses", new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468) });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), "", "Credit Card", new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Hash", "Salt", "UpdatedAt", "UserName" },
                values: new object[] { 1, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), "foo@bar.com", new byte[] { 108, 63, 236, 114, 165, 202, 232, 51, 48, 236, 80, 221, 117, 53, 66, 19, 89, 246, 165, 30, 189, 233, 107, 90, 46, 214, 106, 163, 204, 19, 56, 102, 22, 217, 10, 21, 68, 30, 134, 3, 212, 189, 169, 153, 10, 105, 217, 32, 122, 16, 33, 133, 115, 82, 162, 56, 253, 252, 235, 29, 165, 157, 241, 29 }, new byte[] { 194, 245, 187, 253, 99, 116, 82, 69, 162, 6, 205, 41, 106, 159, 111, 138 }, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), "test" });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "Value" },
                values: new object[] { 1, 1, null, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), new DateTime(2019, 10, 28, 8, 34, 52, 488, DateTimeKind.Local).AddTicks(6617), 1, new DateTime(2019, 10, 28, 8, 34, 52, 476, DateTimeKind.Local).AddTicks(3468), 10m });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TypeId",
                table: "Expenses",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExpenseCateogries");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");
        }
    }
}
