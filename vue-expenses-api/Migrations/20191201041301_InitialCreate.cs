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
                    CurrencyRegionName = table.Column<string>(nullable: true),
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
                columns: new[] { "Id", "Archived", "CreatedAt", "CurrencyRegionName", "Email", "FirstName", "FullName", "Hash", "LastName", "Salt", "SystemName", "UpdatedAt", "UseDarkMode" },
                values: new object[] { 1, false, new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), "GB", "test@demo.com", "John", "John Doe", new byte[] { 194, 172, 90, 162, 216, 129, 48, 228, 254, 41, 251, 116, 49, 71, 13, 67, 230, 168, 211, 146, 178, 51, 94, 226, 100, 127, 10, 42, 64, 153, 188, 159, 113, 194, 131, 22, 7, 69, 43, 128, 222, 49, 80, 103, 193, 26, 238, 98, 123, 146, 177, 116, 121, 196, 129, 191, 29, 220, 34, 21, 97, 101, 149, 11 }, "Doe", new byte[] { 126, 180, 131, 20, 206, 40, 125, 73, 178, 238, 120, 120, 75, 133, 52, 211 }, "VueExpenses", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "General Expenses", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Shopping", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Utilities", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Travel", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Credit Card", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Debit Card", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Cheque", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), null, "Cash", new DateTime(2019, 12, 1, 4, 13, 1, 151, DateTimeKind.Local).AddTicks(8866), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 2, null, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 577.940990020494m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 4, null, new DateTime(2019, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1331.96522241084m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 3, null, new DateTime(2019, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 405.131186780115m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 4, null, new DateTime(2019, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1135.50295407674m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 2, null, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1158.38754044771m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 4, null, new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 724.280389595908m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 3, null, new DateTime(2019, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 708.177182687529m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 2, null, new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 756.333193162611m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 4, null, new DateTime(2019, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 25.9122185064071m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 2, null, new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 606.666210157176m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 3, null, new DateTime(2019, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 932.509800853445m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 2, null, new DateTime(2019, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1198.00338554103m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 2, null, new DateTime(2019, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1129.69324883525m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 4, null, new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1040.55405689429m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 2, null, new DateTime(2019, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1010.42711781823m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 4, null, new DateTime(2019, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 172.963852841856m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 4, null, new DateTime(2019, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1218.31356814053m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 4, null, new DateTime(2019, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 511.044145566898m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 2, null, new DateTime(2019, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 409.686095970537m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 3, null, new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 355.686532731953m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 3, null, new DateTime(2019, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1377.17680930029m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 3, null, new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 668.240814315267m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 2, null, new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 721.648503430955m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 4, null, new DateTime(2019, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 420.963945994602m });

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
