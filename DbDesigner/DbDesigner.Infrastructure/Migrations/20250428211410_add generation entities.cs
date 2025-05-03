using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addgenerationentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenerationLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerationLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenerationModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenerationModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "GenerationLanguages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "English" },
                    { 2, "Spanish" },
                    { 3, "French" },
                    { 4, "Russian" }
                });

            migrationBuilder.InsertData(
                table: "GenerationModels",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "GPT-4" },
                    { 2, "DeepSeek V3" },
                    { 3, "DeepSeek R1" },
                    { 4, "Gemini 2.5 Pro" },
                    { 5, "Llama 4 Maverick" },
                    { 6, "Qwen" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 28, 21, 14, 9, 352, DateTimeKind.Utc).AddTicks(7990), "$2a$11$tWeckwR8ac0CdqPdZ/J6geon2Bxs0OfQDKN1EloQeNelA9qT/RMUi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 28, 21, 14, 9, 537, DateTimeKind.Utc).AddTicks(4560), "$2a$11$//JJ7Gu3dU/5dgxCW/oVguOeBsNILOPUEvZ1rh3b8eLMql5zICQFe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenerationLanguages");

            migrationBuilder.DropTable(
                name: "GenerationModels");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 17, 15, 25, 45, 546, DateTimeKind.Utc).AddTicks(7690), "$2a$11$9uozbV6fPeh18jONPxN/C.EzH.15MpEwrLQ6nrbtKQdlrzAaKo2qi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "PasswordHash" },
                values: new object[] { new DateTime(2025, 4, 17, 15, 25, 45, 729, DateTimeKind.Utc).AddTicks(6090), "$2a$11$k.n0W/UGlSqKgN2lUNPsReA1HDa/6TbhgaMhVQHm2.45RwPNKemmq" });
        }
    }
}
