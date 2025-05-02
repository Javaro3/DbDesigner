using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateprojecttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectTables");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Tables",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tables_ProjectId",
                table: "Tables",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Projects_ProjectId",
                table: "Tables",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Projects_ProjectId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_ProjectId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Tables");

            migrationBuilder.CreateTable(
                name: "ProjectTables",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    TableId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTables", x => new { x.ProjectId, x.TableId });
                    table.ForeignKey(
                        name: "FK_ProjectTables_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTables_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTables_TableId",
                table: "ProjectTables",
                column: "TableId");
        }
    }
}
