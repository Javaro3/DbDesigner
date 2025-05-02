using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixcolumnproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnProperty");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 23, 43, 8, 289, DateTimeKind.Utc).AddTicks(3230), "$2a$11$PwYcyDJZ0Bmvh2bbWlzNYeBHqIt5IUJV6EfyxfEPoltLgN1r7XI8C" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 23, 43, 8, 474, DateTimeKind.Utc).AddTicks(8390), "$2a$11$Y/KL0FvlK7SDZusExT1aAOBKyty.5PiK7zKY6xFU0qd8/Kk2oxiiG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnProperty",
                columns: table => new
                {
                    ColumnsId = table.Column<int>(type: "integer", nullable: false),
                    PropertiesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnProperty", x => new { x.ColumnsId, x.PropertiesId });
                    table.ForeignKey(
                        name: "FK_ColumnProperty_Columns_ColumnsId",
                        column: x => x.ColumnsId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnProperty_Properties_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 23, 11, 48, 139, DateTimeKind.Utc).AddTicks(2040), "$2a$11$8wc.1oVVgBaefHd22ITjnOBJs8bb3NICg5ZOlwid/mLkcZHoRp8Te" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 23, 11, 48, 321, DateTimeKind.Utc).AddTicks(8930), "$2a$11$.2.KSSdbdUloJSXd6K8vweXUiTNzL8FO0CgjKDpJsG5UZYf8Tr/QO" });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnProperty_PropertiesId",
                table: "ColumnProperty",
                column: "PropertiesId");
        }
    }
}
