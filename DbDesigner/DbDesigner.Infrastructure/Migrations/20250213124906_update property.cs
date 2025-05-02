using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataBaseId",
                table: "Properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_DataBaseId",
                table: "Properties",
                column: "DataBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_DataBases_DataBaseId",
                table: "Properties",
                column: "DataBaseId",
                principalTable: "DataBases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_DataBases_DataBaseId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_DataBaseId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DataBaseId",
                table: "Properties");
        }
    }
}
