using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace vue_expenses_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SystemName = table.Column<string>(nullable: true),
                    UseDarkMode = table.Column<bool>(nullable: false),
                    Hash = table.Column<byte[]>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Budget = table.Column<decimal>(nullable: false),
                    ColourHex = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseTypes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Archived = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    TypeId = table.Column<int>(nullable: true),
                    Value = table.Column<decimal>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Archived", "CreatedAt", "Email", "FirstName", "FullName", "Hash", "LastName", "Salt", "SystemName", "UpdatedAt", "UseDarkMode" },
                values: new object[] { 1, false, new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), "test@demo.com", "John", "John Doe", new byte[] { 111, 209, 94, 38, 124, 54, 81, 184, 61, 106, 133, 236, 16, 4, 211, 153, 75, 49, 182, 85, 126, 174, 27, 100, 2, 197, 200, 107, 180, 100, 124, 255, 28, 7, 242, 191, 185, 178, 150, 39, 159, 251, 191, 120, 94, 252, 230, 191, 100, 49, 247, 236, 130, 9, 24, 43, 223, 24, 89, 97, 222, 248, 31, 156 }, "Doe", new byte[] { 237, 49, 99, 254, 190, 29, 86, 64, 139, 32, 160, 208, 150, 248, 183, 184 }, "VueExpenses", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "General Expenses", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Shopping", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Utilities", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Travel", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Credit Card", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Debit Card", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Cheque", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), null, "Cash", new DateTime(2019, 11, 30, 5, 5, 37, 516, DateTimeKind.Local).AddTicks(5044), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 3, null, new DateTime(2019, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 95.0586225814459m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 3, null, new DateTime(2019, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 569.429925908069m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 3, null, new DateTime(2019, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 791.063266010519m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 4, null, new DateTime(2019, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 926.95278833944m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 2, null, new DateTime(2019, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1225.22129361761m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 2, null, new DateTime(2019, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 57.5379180058548m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 2, null, new DateTime(2019, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 273.147948446287m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 3, null, new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1037.87581531232m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 4, null, new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 693.004991483411m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 4, null, new DateTime(2019, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 62.4153886746687m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 4, null, new DateTime(2019, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 429.541639950844m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 2, null, new DateTime(2019, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 558.802198646032m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 3, null, new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 516.605302932023m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 3, null, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 733.048331100982m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 2, null, new DateTime(2019, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 300.668819714649m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 3, null, new DateTime(2019, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1435.69466748074m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 4, null, new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 87.4004010518084m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 2, null, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1183.62335124222m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 3, null, new DateTime(2019, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 706.41427566596m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 2, null, new DateTime(2019, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 216.697256181714m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 4, null, new DateTime(2019, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 140.770092904926m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 2, null, new DateTime(2019, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 650.345050101329m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 4, null, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 520.646970030222m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 2, null, new DateTime(2019, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1005.15954289826m });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_UserId",
                table: "ExpenseCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TypeId",
                table: "Expenses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseTypes_UserId",
                table: "ExpenseTypes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
