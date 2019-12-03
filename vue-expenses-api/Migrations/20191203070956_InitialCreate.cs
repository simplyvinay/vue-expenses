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
                values: new object[] { 1, false, new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), "GB", "test@demo.com", "John", "John Doe", new byte[] { 87, 187, 231, 98, 18, 94, 234, 113, 35, 110, 119, 13, 89, 84, 4, 174, 99, 10, 173, 17, 131, 198, 189, 178, 105, 37, 173, 28, 112, 254, 45, 92, 142, 10, 75, 183, 131, 93, 232, 147, 9, 253, 122, 45, 214, 157, 245, 101, 78, 86, 210, 97, 54, 142, 199, 227, 150, 150, 50, 113, 32, 25, 213, 79 }, "Doe", new byte[] { 52, 239, 132, 228, 105, 110, 101, 66, 183, 165, 173, 144, 153, 137, 12, 116 }, "VueExpenses", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "General Expenses", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Shopping", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Utilities", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Travel", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Credit Card", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Debit Card", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Cheque", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), null, "Cash", new DateTime(2019, 12, 3, 7, 9, 55, 787, DateTimeKind.Local).AddTicks(9411), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 3, null, new DateTime(2019, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1403.46750426268m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 2, null, new DateTime(2019, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 922.086741506162m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 4, null, new DateTime(2019, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 850.961542618909m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 3, null, new DateTime(2019, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 334.451731217304m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 3, null, new DateTime(2019, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 672.835299360024m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 4, null, new DateTime(2019, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 835.003490017263m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 4, null, new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 791.256844667372m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 3, null, new DateTime(2019, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 228.618344631334m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 3, null, new DateTime(2019, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 440.349611658766m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 4, null, new DateTime(2019, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 343.066516492081m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 3, null, new DateTime(2019, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 854.321417330914m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 4, null, new DateTime(2019, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1454.17837749896m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 2, null, new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 492.141799299112m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 4, null, new DateTime(2019, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1091.68861228586m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 2, null, new DateTime(2019, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 168.342807641413m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 4, null, new DateTime(2019, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1391.87224064575m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 2, null, new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 758.134676496561m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 2, null, new DateTime(2019, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 530.740105794156m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 2, null, new DateTime(2019, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 833.508845108333m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 2, null, new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1103.60175958071m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 3, null, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1126.86389969981m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 2, null, new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 99.009541607932m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 3, null, new DateTime(2019, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1349.4523751314m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 4, null, new DateTime(2019, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1129.74741199508m });

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
