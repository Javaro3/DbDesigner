using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatesqltypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataBaseTypes");

            migrationBuilder.AddColumn<int>(
                name: "DataBaseId",
                table: "SqlTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SqlTypes_DataBaseId",
                table: "SqlTypes",
                column: "DataBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SqlTypes_DataBases_DataBaseId",
                table: "SqlTypes",
                column: "DataBaseId",
                principalTable: "DataBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SqlTypes_DataBases_DataBaseId",
                table: "SqlTypes");

            migrationBuilder.DropIndex(
                name: "IX_SqlTypes_DataBaseId",
                table: "SqlTypes");

            migrationBuilder.DropColumn(
                name: "DataBaseId",
                table: "SqlTypes");

            migrationBuilder.CreateTable(
                name: "DataBaseTypes",
                columns: table => new
                {
                    DataBaseId = table.Column<int>(type: "integer", nullable: false),
                    SqlTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBaseTypes", x => new { x.DataBaseId, x.SqlTypeId });
                    table.ForeignKey(
                        name: "FK_DataBaseTypes_DataBases_DataBaseId",
                        column: x => x.DataBaseId,
                        principalTable: "DataBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBaseTypes_SqlTypes_SqlTypeId",
                        column: x => x.SqlTypeId,
                        principalTable: "SqlTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataBaseTypes_SqlTypeId",
                table: "DataBaseTypes",
                column: "SqlTypeId");
        }
    }
}
