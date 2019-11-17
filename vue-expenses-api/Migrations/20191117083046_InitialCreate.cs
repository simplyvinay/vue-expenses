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
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "FullName", "Hash", "LastName", "Salt", "UpdatedAt", "UseDarkMode" },
                values: new object[] { 1, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), "test@demo.com", "John", "John Doe", new byte[] { 43, 5, 112, 192, 163, 4, 75, 112, 86, 234, 40, 136, 196, 133, 139, 2, 85, 142, 2, 205, 39, 242, 203, 230, 25, 45, 114, 48, 209, 93, 186, 50, 221, 94, 125, 109, 245, 209, 251, 3, 46, 59, 241, 189, 52, 27, 98, 38, 171, 78, 166, 213, 80, 78, 9, 30, 217, 51, 222, 125, 5, 180, 242, 1 }, "Doe", new byte[] { 189, 250, 157, 222, 218, 198, 113, 75, 151, 113, 167, 74, 133, 30, 57, 222 }, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), true });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, 2000m, "#CE93D8", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "General Expenses", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, 3000m, "#64B5F6", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Shopping", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, 2500m, "#26A69A", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Utilities", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseCategories",
                columns: new[] { "Id", "Budget", "ColourHex", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, 1000m, "#FB8C00", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Travel", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 1, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Credit Card", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 2, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Debit Card", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 3, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Cheque", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt", "UserId" },
                values: new object[] { 4, new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), null, "Cash", new DateTime(2019, 11, 17, 8, 30, 46, 265, DateTimeKind.Local).AddTicks(8789), 1 });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 3, 2, null, new DateTime(2019, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1108.79055508822m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 19, 3, null, new DateTime(2019, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1003.3220823404m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 17, 4, null, new DateTime(2019, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 200.672735087887m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 15, 3, null, new DateTime(2019, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 404.688618799992m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 14, 3, null, new DateTime(2019, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 752.518428374323m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 2, 2, null, new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 604.672825943992m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 22, 4, null, new DateTime(2019, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 444.396710928714m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 21, 4, null, new DateTime(2019, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 352.764492320253m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 18, 2, null, new DateTime(2019, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1420.60861569858m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 12, 3, null, new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5.38829807442999m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 11, 3, null, new DateTime(2019, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1312.17967430697m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 10, 3, null, new DateTime(2019, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 42.9171777995849m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 6, 3, null, new DateTime(2019, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 719.646415309816m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 5, 3, null, new DateTime(2019, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 61.2630034150849m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 1, 2, null, new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2019, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 986.41200968363m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 24, 2, null, new DateTime(2019, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1156.0146096889m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 16, 2, null, new DateTime(2019, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1070.64137331706m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 13, 3, null, new DateTime(2019, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 119.455411154523m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 9, 4, null, new DateTime(2019, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1199.82221662059m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 8, 4, null, new DateTime(2019, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1111.59959626924m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 7, 3, null, new DateTime(2019, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 959.127501099896m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 4, 3, null, new DateTime(2019, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2019, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 664.209720289432m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 20, 2, null, new DateTime(2019, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 875.88336801896m });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "CategoryId", "Comments", "CreatedAt", "Date", "TypeId", "UpdatedAt", "UserId", "Value" },
                values: new object[] { 23, 4, null, new DateTime(2019, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2019, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 124.041474482064m });

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
