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
                values: new object[] { 1, false, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), "test@demo.com", "John", "John Doe", new byte[] { 111, 239, 234, 96, 220, 250, 56, 107, 177, 118, 228, 64, 136, 185, 179, 228, 42, 188, 123, 179, 125, 77, 179, 238, 126, 68, 202, 244, 251, 70, 176, 239, 31, 100, 145, 230, 127, 55, 147, 36, 197, 175, 171, 170, 219, 224, 12, 148, 90, 62, 4, 197, 115, 70, 161, 46, 57, 75, 47, 132, 106, 94, 29, 237 }, "Doe", new byte[] { 237, 119, 226, 239, 248, 219, 13, 78, 169, 115, 96, 56, 10, 235, 12, 4 }, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, 2000m, "#CE93D8", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "General Expenses", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, 3000m, "#64B5F6", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Shopping", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, 2500m, "#26A69A", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Utilities", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Archived", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, 1000m, "#FB8C00", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Travel", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, false, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Credit Card", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, false, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Debit Card", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, false, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Cheque", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Archived", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, false, new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), null, "Cash", new DateTime(2019, 11, 19, 12, 32, 10, 36, DateTimeKind.Local).AddTicks(906), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, false, 2, null, new DateTime(2019, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 963.867246622158m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, false, 2, null, new DateTime(2019, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 747.423422870889m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, false, 3, null, new DateTime(2019, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 796.424582040135m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, false, 3, null, new DateTime(2019, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 342.493018294914m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, false, 3, null, new DateTime(2019, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1458.29569965522m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, false, 3, null, new DateTime(2019, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1223.54402799324m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, false, 2, null, new DateTime(2019, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 541.629051576196m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, false, 2, null, new DateTime(2019, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 474.97991611016m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, false, 3, null, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 553.627276585264m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, false, 3, null, new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 585.726959437936m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, false, 2, null, new DateTime(2019, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 371.985867792734m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, false, 2, null, new DateTime(2019, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1357.58145146891m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, false, 3, null, new DateTime(2019, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 121.676726556233m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, false, 4, null, new DateTime(2019, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 747.297437278227m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, false, 2, null, new DateTime(2019, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1495.55849376859m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, false, 2, null, new DateTime(2019, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 965.889313009516m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, false, 3, null, new DateTime(2019, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1274.91615795294m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, false, 3, null, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1295.98594144731m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, false, 2, null, new DateTime(2019, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1465.84019063313m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, false, 2, null, new DateTime(2019, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1458.72799002506m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, false, 2, null, new DateTime(2019, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 238.027202774783m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, false, 4, null, new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1048.50222591707m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, false, 3, null, new DateTime(2019, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1329.29938604557m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Archived", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, false, 2, null, new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 780.691223116914m });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
