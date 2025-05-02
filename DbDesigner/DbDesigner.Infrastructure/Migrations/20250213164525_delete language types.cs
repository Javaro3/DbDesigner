using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletelanguagetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LanguageTypeSqlTypes");

            migrationBuilder.DropTable(
                name: "LanguageTypes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LanguageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageTypes_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTypeSqlTypes",
                columns: table => new
                {
                    LanguageTypeId = table.Column<int>(type: "integer", nullable: false),
                    SqlTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTypeSqlTypes", x => new { x.LanguageTypeId, x.SqlTypeId });
                    table.ForeignKey(
                        name: "FK_LanguageTypeSqlTypes_LanguageTypes_LanguageTypeId",
                        column: x => x.LanguageTypeId,
                        principalTable: "LanguageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageTypeSqlTypes_SqlTypes_SqlTypeId",
                        column: x => x.SqlTypeId,
                        principalTable: "SqlTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypes_LanguageId",
                table: "LanguageTypes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypeSqlTypes_SqlTypeId",
                table: "LanguageTypeSqlTypes",
                column: "SqlTypeId");
        }
    }
}
