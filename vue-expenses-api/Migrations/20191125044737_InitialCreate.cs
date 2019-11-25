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
                columns: new[] { "Id", "Archived", "CreatedAt", "Email", "FirstName", "FullName", "Hash", "LastName", "Salt", "UpdatedAt", "UseDarkMode" },
                values: new object[] { 1, false, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), "test@demo.com", "John", "John Doe", new byte[] { 101, 82, 137, 122, 217, 80, 111, 2, 33, 136, 86, 230, 36, 123, 139, 119, 207, 101, 193, 76, 175, 214, 50, 139, 59, 97, 224, 44, 163, 237, 175, 251, 187, 33, 230, 77, 230, 155, 207, 99, 53, 1, 232, 235, 72, 12, 227, 119, 243, 79, 219, 179, 58, 23, 183, 213, 162, 226, 48, 70, 85, 12, 111, 71 }, "Doe", new byte[] { 25, 70, 37, 112, 172, 210, 101, 75, 174, 193, 187, 225, 154, 132, 5, 107 }, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "General Expenses", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Shopping", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Utilities", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Travel", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Credit Card", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Debit Card", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Cheque", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), null, "Cash", new DateTime(2019, 11, 25, 4, 47, 36, 948, DateTimeKind.Local).AddTicks(354), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 2, null, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 834.733957347802m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 4, null, new DateTime(2019, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 39.9421092774449m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 2, null, new DateTime(2019, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1033.30513277711m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 3, null, new DateTime(2019, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 102.233988513348m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 3, null, new DateTime(2019, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 448.488216823194m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 2, null, new DateTime(2019, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 195.825609004044m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 4, null, new DateTime(2019, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 146.792317110483m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 2, null, new DateTime(2019, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 183.956792198101m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 3, null, new DateTime(2019, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1249.70522255157m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 2, null, new DateTime(2019, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 242.583433511939m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 4, null, new DateTime(2019, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1005.37289306772m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 4, null, new DateTime(2019, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 161.579044843828m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 2, null, new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 593.344512671858m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 2, null, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1499.34558570354m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 4, null, new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 274.332978424771m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 4, null, new DateTime(2019, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 511.151510761656m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 3, null, new DateTime(2019, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 45.5323595300002m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 2, null, new DateTime(2019, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1385.74551576085m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 3, null, new DateTime(2019, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 702.721334855408m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 3, null, new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 243.119241782985m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 4, null, new DateTime(2019, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 598.494613123357m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 2, null, new DateTime(2019, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 166.989741459018m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 2, null, new DateTime(2019, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 692.028396386666m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 4, null, new DateTime(2019, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 426.361364976671m });

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
