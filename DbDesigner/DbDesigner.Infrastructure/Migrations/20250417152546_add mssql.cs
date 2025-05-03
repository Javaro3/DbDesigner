using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmssql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DataBases",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[] { 4, "A high-performance relational database by Microsoft with robust security, enterprise-grade features, and deep integration with Windows ecosystem and Azure cloud services.", "MsSql.png", "MsSql" });

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "UNIQUE");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "FULLTEXT");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "HASH");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "UNIQUE");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "AUTO_INCREMENT");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "GENERATED ALWAYS AS IDENTITY");

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

            migrationBuilder.InsertData(
                table: "IndexTypes",
                columns: new[] { "Id", "DataBaseId", "Description", "Name" },
                values: new object[,]
                {
                    { 9, 4, "Determines the physical order of data in the table. The table can have only one cluster index.", "CLUSTERED" },
                    { 10, 4, "A separate structure that stores a copy of the indexed columns with an indicator to the data. The table can have up to 999 non -lasterized indices.", "NONCLUSTERED" },
                    { 11, 4, "Guarantees the uniqueness of values in indexed columns.", "UNIQUE NONCLUSTERED" },
                    { 12, 4, "Guarantees the uniqueness of values in indexed columns.", "UNIQUE CLUSTERED" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "DataBaseId", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 19, 4, "Allows the column to contain NULL values. NULL means the absence of data or unknown value.", false, "NULL" },
                    { 20, 4, "Prohibits the column contain NULL values. The column must always matter.", false, "NOT NULL" },
                    { 21, 4, "Automatically generates unique increasing numbers for the column. Parameters: initial value and extension step.", true, "IDENTITY" },
                    { 22, 4, "The primary key uniquely identifies each line of the table. Automatically creates a cluster index.", false, "PRIMARY KEY" },
                    { 23, 4, "Provides the uniqueness of the values in the column. Creates a non -lasterized index to verify uniqueness.", false, "UNIQUE" },
                    { 24, 4, "A restriction that checks the values of the column according to a given condition. Parameters: Logical expression for verification.", true, "CHECK" },
                    { 25, 4, "Sets the default value for the column when inserting a new line, if the value is not indicated clearly. Parameters: meaning or expression.", true, "DEFAULT" }
                });

            migrationBuilder.InsertData(
                table: "SqlTypes",
                columns: new[] { "Id", "DataBaseId", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 43, 4, "1-byte integer storing whole numbers from 0 to 255.", false, "TINYINT" },
                    { 44, 4, "2-byte integer storing whole numbers from -32,768 to 32,767.", false, "SMALLINT" },
                    { 45, 4, "4-byte integer storing whole numbers from -2,147,483,648 to 2,147,483,647.", false, "INT" },
                    { 46, 4, "8-byte integer storing whole numbers from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.", false, "BIGINT" },
                    { 49, 4, "Fixed precision and scale numeric data with user-defined precision (max 38 digits).", true, "DECIMAL" },
                    { 50, 4, "Functionally equivalent to DECIMAL - fixed precision numeric data.", true, "NUMERIC" },
                    { 51, 4, "8-byte currency value from -922,337,203,685,477.5808 to 922,337,203,685,477.5807.", false, "MONEY" },
                    { 52, 4, "4-byte currency value from -214,748.3648 to 214,748.3647.", false, "SMALLMONEY" },
                    { 53, 4, "Fixed-length non-Unicode character data (max 8,000 chars).", true, "CHAR" },
                    { 54, 4, "Fixed-length Unicode character data (max 4,000 chars).", true, "NCHAR" },
                    { 55, 4, "Variable-length non-Unicode character data (max 8,000 chars, or MAX for 2GB).", true, "VARCHAR" },
                    { 56, 4, "Variable-length Unicode character data (max 4,000 chars, or MAX for 1GB).", true, "NVARCHAR" },
                    { 57, 4, "Legacy variable-length non-Unicode data (deprecated, use VARCHAR(MAX) instead).", false, "TEXT" },
                    { 58, 4, "Legacy variable-length Unicode data (deprecated, use NVARCHAR(MAX) instead).", false, "NTEXT" },
                    { 59, 4, "Date only (0001-01-01 through 9999-12-31).", false, "DATE" },
                    { 60, 4, "Time only with user-defined fractional second precision.", true, "TIME" },
                    { 61, 4, "Date and time (1753-01-01 through 9999-12-31) with 3.33ms accuracy.", false, "DATETIME" },
                    { 62, 4, "Date and time with higher precision (0001-01-01 through 9999-12-31) and user-defined fractional seconds.", true, "DATETIME2" },
                    { 63, 4, "Compact date and time (1900-01-01 through 2079-06-06) with 1-minute accuracy.", false, "SMALLDATETIME" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "DataBases",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Unique Index");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Full-text Index");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Hash");

            migrationBuilder.UpdateData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Unique Index");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "AUTO INCREMENT");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "GENERATED AS IDENTITY");

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
    }
}
