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
                values: new object[] { 1, false, new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), "GB", "test@demo.com", "John", "John Doe", new byte[] { 180, 148, 181, 234, 119, 66, 64, 255, 188, 216, 171, 34, 96, 152, 156, 57, 236, 236, 206, 115, 206, 89, 215, 98, 90, 63, 116, 27, 212, 87, 21, 14, 27, 14, 24, 164, 244, 218, 13, 142, 100, 206, 107, 78, 185, 197, 188, 61, 254, 164, 245, 5, 44, 145, 36, 166, 131, 242, 200, 34, 216, 234, 183, 55 }, "Doe", new byte[] { 81, 140, 156, 185, 25, 149, 112, 64, 170, 29, 97, 209, 127, 212, 127, 25 }, "VueExpenses", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "General Expenses", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Shopping", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Utilities", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Travel", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Credit Card", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Debit Card", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Cheque", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), null, "Cash", new DateTime(2019, 11, 30, 11, 11, 16, 787, DateTimeKind.Local).AddTicks(6977), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 4, null, new DateTime(2019, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 105.833950967451m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 2, null, new DateTime(2019, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1115.76661612641m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 3, null, new DateTime(2019, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1014.34336370525m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 2, null, new DateTime(2019, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 99.6778943108758m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 4, null, new DateTime(2019, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 651.29653301616m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 4, null, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1147.95312711408m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 3, null, new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1087.71644234085m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 2, null, new DateTime(2019, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1452.83474724406m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 3, null, new DateTime(2019, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 491.45149369326m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 4, null, new DateTime(2019, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1481.09623369812m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 4, null, new DateTime(2019, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1201.57336453049m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 3, null, new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 94.9888620967925m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 3, null, new DateTime(2019, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 452.825443797198m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 2, null, new DateTime(2019, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 917.354376948138m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 4, null, new DateTime(2019, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 615.049501934578m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 4, null, new DateTime(2019, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 415.896273178932m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 2, null, new DateTime(2019, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 129.617189816021m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 4, null, new DateTime(2019, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1079.02304459318m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 2, null, new DateTime(2019, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1036.16591893889m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 2, null, new DateTime(2019, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1225.00458090799m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 3, null, new DateTime(2019, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 241.126274569485m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 2, null, new DateTime(2019, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1114.26524543868m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 4, null, new DateTime(2019, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1399.78958871206m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 3, null, new DateTime(2019, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 96.7421746331929m });

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
