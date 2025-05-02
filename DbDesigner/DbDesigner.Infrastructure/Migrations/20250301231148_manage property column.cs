using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class managepropertycolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnProperties",
                table: "ColumnProperties");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ColumnProperties",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnProperties",
                table: "ColumnProperties",
                column: "Id");

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
                name: "IX_ColumnProperties_ColumnId",
                table: "ColumnProperties",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnProperty_PropertiesId",
                table: "ColumnProperty",
                column: "PropertiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColumnProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnProperties",
                table: "ColumnProperties");

            migrationBuilder.DropIndex(
                name: "IX_ColumnProperties_ColumnId",
                table: "ColumnProperties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ColumnProperties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnProperties",
                table: "ColumnProperties",
                columns: new[] { "ColumnId", "PropertyId" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 19, 5, 5, 52, DateTimeKind.Utc).AddTicks(2980), "$2a$11$ErTwkNFa8slGXV1hZUgZtO0/2r/M7EzSTV/r1lLo70ag9f0syHKku" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 1, 19, 5, 5, 256, DateTimeKind.Utc).AddTicks(5270), "$2a$11$Eoe0PWyqTuvM0fFWMPzRauC9HrodntQvx.LekZmUAzopcnx/22rI2" });
        }
    }
}
