using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbDesigner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class datageneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Architectures",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An approach in which code interacts directly with the database, without the use of abstraction layers.", "Direct Data Access (DDA)" },
                    { 2, "A design pattern that provides an abstraction for working with data, allowing data access logic to be separated from business logic.", "DbDesigner.Infrastructure Pattern" },
                    { 3, "An architectural pattern that separates state change operations (commands) from data requests (reads), ensuring their independence.", "Command Query Responsibility Segregation (CQRS)" }
                });

            migrationBuilder.InsertData(
                table: "DataBases",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "An open object-relational DBMS with support for complex queries, extensions and highly reliable transactions.", "PostgreSQL.png", "PostgreSQL" },
                    { 2, "A popular and fast open DBMS, widely used in web applications due to its ease of configuration and cross-platform functionality.", "MySQL.png", "MySQL" },
                    { 3, "A lightweight embedded DBMS that does not require a server, ideal for mobile applications and prototyping.", "SQLite.png", "SQLite" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "An object-oriented programming language from Microsoft, widely used for developing enterprise applications, games and web services.", "cs.png", "C#" },
                    { 2, "A high-level programming language with a simple syntactic structure, popular for development, automation, data analysis and machine learning.", "py.png", "Python" },
                    { 3, "A scripting language for developing interactive web pages, also actively used in server and mobile applications.", "js.png", "JavaScript" }
                });

            migrationBuilder.InsertData(
                table: "RelationActions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "When you delete or update a parent record, the associated child records are automatically deleted or updated.", "CASCADE" },
                    { 2, "When a parent record is deleted or updated, the corresponding foreign keys in the child records are set to NULL.", "SET NULL" },
                    { 3, "When a parent record is deleted or updated, the foreign keys in the child records are set to the default value.", "SET DEFAULT" },
                    { 4, "Prevents a parent record from being deleted or updated if there are associated child records.", "RESTRICT" },
                    { 5, "Similar to RESTRICT, but data integrity checks are performed later, after all transaction actions.", "NO ACTION" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Administrator" },
                    { 2, "", "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 13, 17, 18, 14, 299, DateTimeKind.Utc).AddTicks(9030), "admin@gmail.com", "admin", "$2a$11$6NjZFJ4PerIqCK5u9f08weyGRGNbe.YedVaCNJ4uZnUsG9uOvuvbi" },
                    { 2, new DateTime(2025, 2, 13, 17, 18, 14, 483, DateTimeKind.Utc).AddTicks(4780), "user@gmail.com", "user", "$2a$11$4uuqVfitIsdZW81J0L8gCeT1uu5AUEN8Y0dnQhi.HGTEnmy.qapeu" }
                });

            migrationBuilder.InsertData(
                table: "IndexTypes",
                columns: new[] { "Id", "DataBaseId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Standard index that improves query performance by allowing faster data retrieval. Data is logically ordered, but not physically stored in the index order.", "Index" },
                    { 2, 3, "Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.", "Unique Index" },
                    { 3, 2, "Default index type in MySQL. Suitable for most queries, including equality checks, range queries, and sorting. Data is stored in a balanced tree structure.", "B-tree" },
                    { 4, 2, "Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.", "Unique Index" },
                    { 5, 2, "Optimized for full-text search queries. Allows efficient searching of text data using natural language queries.", "Full-text Index" },
                    { 6, 1, "Default index type in PostgreSQL. Suitable for most queries, including equality checks, range queries, and sorting. Data is stored in a balanced tree structure.", "B-tree" },
                    { 7, 1, "Optimized for equality checks (=). Provides fast lookups but does not support range queries or sorting. Suitable for exact match queries.", "Hash" },
                    { 8, 1, "Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.", "Unique Index" }
                });

            migrationBuilder.InsertData(
                table: "Orms",
                columns: new[] { "Id", "Description", "LanguageId", "Name" },
                values: new object[,]
                {
                    { 1, "An ORM for C# and .NET that provides a high level of abstraction for working with databases, allowing you to use LINQ for queries.", 1, "Entity Framework" },
                    { 2, "A popular ORM for Python that supports powerful mapping and database schema management.", 2, "SQLAlchemy" },
                    { 3, "ORM for Node.js with support for various relational databases and a powerful interface for working with SQL queries.", 3, "Sequelize" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "DataBaseId", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.", false, "PRIMARY KEY" },
                    { 2, 3, "Ensures that all values in the column are unique. Prevents duplicate entries.", false, "UNIQUE" },
                    { 3, 3, "Ensures that the column cannot contain NULL values.", false, "NOT NULL" },
                    { 4, 3, "Sets a default value for the column if no value is provided during insertion.", true, "DEFAULT" },
                    { 5, 3, "Enforces a condition that must be true for all values in the column.", true, "CHECK" },
                    { 6, 2, "Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.", false, "PRIMARY KEY" },
                    { 7, 2, "Automatically generates a unique value for the column, incrementing with each new row.", false, "AUTO INCREMENT" },
                    { 8, 2, "Ensures that all values in the column are unique. Prevents duplicate entries.", false, "UNIQUE" },
                    { 9, 2, "Ensures that the column cannot contain NULL values.", false, "NOT NULL" },
                    { 10, 2, "Sets a default value for the column if no value is provided during insertion.", true, "DEFAULT" },
                    { 11, 2, "Enforces a condition that must be true for all values in the column.", true, "CHECK" },
                    { 12, 2, "Restricts the column to non-negative numeric values.", false, "UNSIGNED" },
                    { 13, 1, "Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.", false, "PRIMARY KEY" },
                    { 14, 1, "Automatically generates a unique value for the column, similar to auto-increment but compliant with SQL standards.", false, "GENERATED AS IDENTITY" },
                    { 15, 1, "Ensures that all values in the column are unique. Prevents duplicate entries.", false, "UNIQUE" },
                    { 16, 1, "Ensures that the column cannot contain NULL values.", false, "NOT NULL" },
                    { 17, 1, "Sets a default value for the column if no value is provided during insertion.", true, "DEFAULT" },
                    { 18, 1, "Enforces a condition that must be true for all values in the column.", true, "CHECK" }
                });

            migrationBuilder.InsertData(
                table: "SqlTypes",
                columns: new[] { "Id", "DataBaseId", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Stores integer values. Flexible storage (1, 2, 3, 4, 6, or 8 bytes depending on magnitude).", false, "INTEGER" },
                    { 2, 3, "Stores floating-point numbers (8-byte IEEE double precision).", false, "REAL" },
                    { 3, 3, "Stores UTF-8/UTF-16 encoded string values.", false, "TEXT" },
                    { 4, 3, "Stores numeric values (integer or floating-point). Behaves like INTEGER/REAL based on context.", false, "NUMERIC" },
                    { 5, 2, "4-byte signed integer. Range: -2,147,483,648 to 2,147,483,647.", false, "INT" },
                    { 6, 2, "1-byte signed integer. Range: -128 to 127. UNSIGNED: 0 to 255.", false, "TINYINT" },
                    { 7, 2, "2-byte signed integer. Range: -32,768 to 32,767. UNSIGNED: 0 to 65,535.", false, "SMALLINT" },
                    { 8, 2, "3-byte signed integer. Range: -8,388,608 to 8,388,607. UNSIGNED: 0 to 16,777,215.", false, "MEDIUMINT" },
                    { 9, 2, "8-byte signed integer. Range: -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.", false, "BIGINT" },
                    { 10, 2, "4-byte floating-point number (single precision). Approximate precision: 7 decimal digits.", false, "FLOAT" },
                    { 11, 2, "8-byte floating-point number (double precision). Approximate precision: 15 decimal digits.", false, "DOUBLE" },
                    { 12, 2, "Fixed-point number with exact precision. Syntax: DECIMAL(M,D) where M=total digits, D=decimal digits.", true, "DECIMAL" },
                    { 13, 2, "Fixed-length string. Padded with spaces to specified length. Max length: 255 characters.", true, "CHAR" },
                    { 14, 2, "Variable-length string. Max length: 65,535 characters.", true, "VARCHAR" },
                    { 15, 2, "Variable-length string for large text. Max size: 65,535 bytes.", false, "TEXT" },
                    { 16, 2, "Variable-length string. Max size: 255 bytes.", false, "TINYTEXT" },
                    { 17, 2, "Variable-length string. Max size: 16,777,215 bytes.", false, "MEDIUMTEXT" },
                    { 18, 2, "Variable-length string. Max size: 4,294,967,295 bytes.", false, "LONGTEXT" },
                    { 19, 2, "Fixed-length binary data. Padded with zeros. Max length: 255 bytes.", true, "BINARY" },
                    { 20, 2, "Variable-length binary data. Max length: 65,535 bytes.", true, "VARBINARY" },
                    { 21, 2, "Binary Large Object. Max size: 65,535 bytes.", false, "BLOB" },
                    { 22, 2, "Binary Large Object. Max size: 255 bytes.", false, "TINYBLOB" },
                    { 23, 2, "Binary Large Object. Max size: 16,777,215 bytes.", false, "MEDIUMBLOB" },
                    { 24, 2, "Binary Large Object. Max size: 4,294,967,295 bytes.", false, "LONGBLOB" },
                    { 25, 2, "Stores date in 'YYYY-MM-DD' format. Range: '1000-01-01' to '9999-12-31'.", false, "DATE" },
                    { 26, 2, "Stores time in 'HH:MM:SS' format. Range: '-838:59:59' to '838:59:59'.", false, "TIME" },
                    { 27, 2, "Stores date and time in 'YYYY-MM-DD HH:MM:SS' format. Range: '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.", false, "DATETIME" },
                    { 28, 2, "Stores UTC timestamp. Range: '1970-01-01 00:00:01' UTC to '2038-01-19 03:14:07' UTC.", false, "TIMESTAMP" },
                    { 29, 1, "4-byte signed integer. Alias for INTEGER. Range: -2,147,483,648 to 2,147,483,647.", false, "INT" },
                    { 30, 1, "2-byte signed integer. Range: -32,768 to 32,767.", false, "SMALLINT" },
                    { 31, 1, "8-byte signed integer. Range: -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.", false, "BIGINT" },
                    { 32, 1, "Exact numeric type. Syntax: DECIMAL(precision, scale). Precision: up to 131072 digits.", true, "DECIMAL" },
                    { 33, 1, "Alias for DECIMAL. Exact numeric type with configurable precision and scale.", true, "NUMERIC" },
                    { 34, 1, "4-byte floating-point number (single precision). Approximate precision: 6 decimal digits.", false, "REAL" },
                    { 35, 1, "Fixed-length string. Padded with spaces. Syntax: CHAR(n). Max length: 1GB.", true, "CHAR" },
                    { 36, 1, "Variable-length string. Syntax: VARCHAR(n). Max length: 1GB.", true, "VARCHAR" },
                    { 37, 1, "Variable-length string with unlimited length (up to 1GB).", false, "TEXT" },
                    { 38, 1, "Binary data (equivalent to BLOB in other databases).", false, "BYTEA" },
                    { 39, 1, "Stores date in 'YYYY-MM-DD' format. Range: 4713 BC to 294276 AD.", false, "DATE" },
                    { 40, 1, "Stores time in 'HH:MM:SS' format. Includes optional time zone.", false, "TIME" },
                    { 41, 1, "Stores date and time (without time zone) in 'YYYY-MM-DD HH:MM:SS' format.", false, "TIMESTAMP" },
                    { 42, 1, "Stores true/false values. Valid inputs: TRUE, FALSE, NULL.", false, "BOOLEAN" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Architectures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Architectures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Architectures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "IndexTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RelationActions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RelationActions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RelationActions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RelationActions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "RelationActions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "SqlTypes",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DataBases",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DataBases",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DataBases",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
