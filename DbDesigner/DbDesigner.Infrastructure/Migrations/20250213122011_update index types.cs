using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateindextypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataBaseIndexTypes");

            migrationBuilder.AddColumn<int>(
                name: "DataBaseId",
                table: "IndexTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IndexTypes_DataBaseId",
                table: "IndexTypes",
                column: "DataBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndexTypes_DataBases_DataBaseId",
                table: "IndexTypes",
                column: "DataBaseId",
                principalTable: "DataBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndexTypes_DataBases_DataBaseId",
                table: "IndexTypes");

            migrationBuilder.DropIndex(
                name: "IX_IndexTypes_DataBaseId",
                table: "IndexTypes");

            migrationBuilder.DropColumn(
                name: "DataBaseId",
                table: "IndexTypes");

            migrationBuilder.CreateTable(
                name: "DataBaseIndexTypes",
                columns: table => new
                {
                    DataBaseId = table.Column<int>(type: "integer", nullable: false),
                    IndexTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBaseIndexTypes", x => new { x.DataBaseId, x.IndexTypeId });
                    table.ForeignKey(
                        name: "FK_DataBaseIndexTypes_DataBases_DataBaseId",
                        column: x => x.DataBaseId,
                        principalTable: "DataBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataBaseIndexTypes_IndexTypes_IndexTypeId",
                        column: x => x.IndexTypeId,
                        principalTable: "IndexTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataBaseIndexTypes_IndexTypeId",
                table: "DataBaseIndexTypes",
                column: "IndexTypeId");
        }
    }
}
