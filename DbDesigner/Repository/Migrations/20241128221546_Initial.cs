using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Architectures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Architectures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataBases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IndexTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HasParams = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SqlTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    HasParams = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataBaseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_DataBases_DataBaseId",
                        column: x => x.DataBaseId,
                        principalTable: "DataBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Indices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IndexTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Indices_IndexTypes_IndexTypeId",
                        column: x => x.IndexTypeId,
                        principalTable: "IndexTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LanguageId = table.Column<int>(type: "integer", nullable: false)
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
                name: "LanguageOrms",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "integer", nullable: false),
                    OrmId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageOrms", x => new { x.LanguageId, x.OrmId });
                    table.ForeignKey(
                        name: "FK_LanguageOrms_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LanguageOrms_Orms_OrmId",
                        column: x => x.OrmId,
                        principalTable: "Orms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Columns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SqlTypeId = table.Column<int>(type: "integer", nullable: false),
                    SqlTypeParams = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Columns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Columns_SqlTypes_SqlTypeId",
                        column: x => x.SqlTypeId,
                        principalTable: "SqlTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserProjects",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjects", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjects_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
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

            migrationBuilder.CreateTable(
                name: "ColumnProperties",
                columns: table => new
                {
                    ColumnId = table.Column<int>(type: "integer", nullable: false),
                    PropertyId = table.Column<int>(type: "integer", nullable: false),
                    PropertyParams = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnProperties", x => new { x.ColumnId, x.PropertyId });
                    table.ForeignKey(
                        name: "FK_ColumnProperties_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnProperties_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IndexColumns",
                columns: table => new
                {
                    IndexId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndexColumns", x => new { x.ColumnId, x.IndexId });
                    table.ForeignKey(
                        name: "FK_IndexColumns_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndexColumns_Indices_IndexId",
                        column: x => x.IndexId,
                        principalTable: "Indices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceColumnId = table.Column<int>(type: "integer", nullable: false),
                    TargetColumnId = table.Column<int>(type: "integer", nullable: false),
                    OnDeleteId = table.Column<int>(type: "integer", nullable: false),
                    OnUpdateId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relations_Columns_SourceColumnId",
                        column: x => x.SourceColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relations_Columns_TargetColumnId",
                        column: x => x.TargetColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relations_RelationActions_OnDeleteId",
                        column: x => x.OnDeleteId,
                        principalTable: "RelationActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Relations_RelationActions_OnUpdateId",
                        column: x => x.OnUpdateId,
                        principalTable: "RelationActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TableColumns",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "integer", nullable: false),
                    ColumnId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableColumns", x => new { x.ColumnId, x.TableId });
                    table.ForeignKey(
                        name: "FK_TableColumns_Columns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "Columns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableColumns_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Architectures",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An approach in which code interacts directly with the database, without the use of abstraction layers.", "Direct Data Access (DDA)" },
                    { 2, "A design pattern that provides an abstraction for working with data, allowing data access logic to be separated from business logic.", "Repository Pattern" },
                    { 3, "An architectural pattern that separates state change operations (commands) from data requests (reads), ensuring their independence.", "Command Query Responsibility Segregation (CQRS)" }
                });

            migrationBuilder.InsertData(
                table: "DataBases",
                columns: new[] { "Id", "Description", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "A commercial DBMS from Microsoft with deep integration into the Windows ecosystem and powerful analytical capabilities.", "MSSQL.png", "MSSQL" },
                    { 2, "An open object-relational DBMS with support for complex queries, extensions and highly reliable transactions.", "PostgreSQL.png", "PostgreSQL" },
                    { 3, "A popular and fast open DBMS, widely used in web applications due to its ease of configuration and cross-platform functionality.", "MySQL.png", "MySQL" },
                    { 4, "A lightweight embedded DBMS that does not require a server, ideal for mobile applications and prototyping.", "SQLite.png", "SQLite" }
                });

            migrationBuilder.InsertData(
                table: "IndexTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Table data is sorted and physically stored in the order specified by the index.", "Clustered Index" },
                    { 2, "Contains pointers to data, rather than the data itself.", "Non-Clustered Index" },
                    { 3, "Ensures that values in a column or set of columns are unique.", "Unique Index" },
                    { 4, "Used to search text in large text data.", "Full-Text Index" },
                    { 5, "The standard index for most search operations.", "B-tree Index" },
                    { 6, "Index for quick search by exact value.", "Hash Index" }
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
                table: "Orms",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "An ORM for C# and .NET that provides a high level of abstraction for working with databases, allowing you to use LINQ for queries.", "Entity Framework" },
                    { 2, "A lightweight mixin for C# that provides high-performance access to databases with minimal wrapper around SQL queries.", "Dapper" },
                    { 3, "A powerful ORM for C# with extensive customization capabilities for mapping objects to relational databases.", "NHibernate" },
                    { 4, "A popular ORM for Python that supports powerful mapping and database schema management.", "SQLAlchemy" },
                    { 5, "A built-in ORM in Django that provides a high-level abstraction for working with databases in Python web applications.", "Django ORM" },
                    { 6, "An asynchronous ORM for Python designed to work with databases using Django ORM-like syntax.", "Tortoise ORM" },
                    { 7, "A lightweight ORM for Python that offers a simple and intuitive model for working with databases.", "Peewee" },
                    { 8, "ORM for Node.js with support for various relational databases and a powerful interface for working with SQL queries.", "Sequelize" },
                    { 9, "ORM for TypeScript and JavaScript, providing support for TypeScript annotations and work with multiple relational databases.", "TypeORM" }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 1, "Prevents a column from being NULL.", false, "NOT NULL" },
                    { 2, "Sets the default value for the column if no value is specified during data insertion.", true, "DEFAULT" },
                    { 3, "Defines the column as a primary key, which must be unique for each row and not contain NULLs.", false, "PRIMARY KEY" },
                    { 4, "Automatically increments the column value with each new entry. Typically used with primary keys.", false, "INCREMENT" },
                    { 5, "Ensures that the values in a column are unique. You cannot insert duplicate values.", false, "UNIQUE" },
                    { 6, "Defines a condition that column values must meet.", true, "CHECK" }
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
                table: "SqlTypes",
                columns: new[] { "Id", "Description", "HasParams", "Name" },
                values: new object[,]
                {
                    { 1, "number from 0 to 255", false, "tinyint" },
                    { 2, "number from 0 to 65 535", false, "smallint" },
                    { 3, "number from 0 to 2 147 483 647", false, "int" },
                    { 4, "number from 0 to 2E64-1", false, "bigint" },
                    { 5, "number from 0 to 1", false, "bit" },
                    { 6, "Numbers with fixed precision and scale. If maximum precision is used, valid values are in the -10^38 + 1 range 10^38 - 1.", true, "decimal" },
                    { 7, "Numbers with fixed precision and scale. If maximum precision is used, valid values are in the -10^38 + 1 range 10^38 - 1.", true, "numeric" },
                    { 8, "number from –922,337,203,685,477.5808 to 922,337,203,685,477.5807", false, "money" },
                    { 9, "number from -214 748,3648 to 214 748,3647", false, "smallmoney" },
                    { 10, "number from -1,79E+308 to 1,79E+308", false, "float" },
                    { 11, "number from -1,79E+308 to 1,79E+308", false, "real" },
                    { 12, "date type", false, "date" },
                    { 13, "time type", false, "time" },
                    { 14, "Specifies a date that includes the time of day in 24-hour format", false, "datetime2" },
                    { 15, "Defines a date that is combined with the time of day and adds time zone awareness based on Coordinated Universal Time (UTC).", false, "datetimeoffset" },
                    { 16, "Avoid using date and time for new work. Use date, date, datetime2, and datetimeoffset data types instead.", false, "datetime" },
                    { 17, "Specifies a date that matches the time of day. Time is presented in 24-hour format with seconds always set to zero (:00), and no fractional seconds.", false, "smalldatetime" },
                    { 18, "Fixed-size string data. n specifies the size of the string in bytes and must be a value between 1 and 8000", true, "char" },
                    { 19, "Fixed-size string data. n specifies the size of the string in bytes and must be a value between 1 and 8000", true, "varchar" },
                    { 20, "This is a fixed and variable length data type designed to store character and binary data in Unicode format", false, "text" },
                    { 21, "This is a fixed and variable length data type designed to store character and binary data in Unicode format", true, "nchar" },
                    { 22, "This is a fixed and variable length data type designed to store character and binary data in Unicode format", true, "nvarchar" },
                    { 23, "This is a fixed and variable length data type designed to store character and binary data in Unicode format", false, "ntext" },
                    { 24, "signed four-byte integer", false, "integer" },
                    { 25, "A medium integer. Signed range is from -8388608 to 8388607.", false, "mediumint" },
                    { 26, "logical Boolean (true/false)", false, "boolean" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "Name", "PasswordHash" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 28, 22, 15, 46, 265, DateTimeKind.Utc).AddTicks(810), "admin@gmail.com", "admin", "$2a$11$sHS5pDpUKamAP/k2seAoyuxHR13MRvo0QcruS5C41RczBhq.ATkvO" },
                    { 2, new DateTime(2024, 11, 28, 22, 15, 46, 424, DateTimeKind.Utc).AddTicks(160), "user@gmail.com", "user", "$2a$11$thP6xStr5tUSkrOdxQOFF.ywygzWwjgWpWKYwW.3pgin6pCz.XsE6" }
                });

            migrationBuilder.InsertData(
                table: "DataBaseIndexTypes",
                columns: new[] { "DataBaseId", "IndexTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 3 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 4, 3 },
                    { 4, 4 },
                    { 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "DataBaseTypes",
                columns: new[] { "DataBaseId", "SqlTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 10 },
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 13 },
                    { 1, 14 },
                    { 1, 15 },
                    { 1, 16 },
                    { 1, 17 },
                    { 1, 18 },
                    { 1, 19 },
                    { 1, 20 },
                    { 1, 21 },
                    { 1, 22 },
                    { 1, 23 },
                    { 2, 2 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 2, 8 },
                    { 2, 11 },
                    { 2, 12 },
                    { 2, 13 },
                    { 2, 18 },
                    { 2, 19 },
                    { 2, 20 },
                    { 2, 21 },
                    { 2, 22 },
                    { 2, 24 },
                    { 2, 26 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 10 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 16 },
                    { 3, 18 },
                    { 3, 19 },
                    { 3, 20 },
                    { 3, 21 },
                    { 3, 22 },
                    { 3, 24 },
                    { 3, 25 },
                    { 3, 26 },
                    { 4, 1 },
                    { 4, 2 },
                    { 4, 3 },
                    { 4, 7 },
                    { 4, 10 },
                    { 4, 11 },
                    { 4, 12 },
                    { 4, 16 },
                    { 4, 19 },
                    { 4, 20 },
                    { 4, 21 },
                    { 4, 22 },
                    { 4, 24 },
                    { 4, 25 },
                    { 4, 26 }
                });

            migrationBuilder.InsertData(
                table: "LanguageOrms",
                columns: new[] { "LanguageId", "OrmId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 2, 7 },
                    { 3, 8 },
                    { 3, 9 }
                });

            migrationBuilder.InsertData(
                table: "LanguageTypes",
                columns: new[] { "Id", "Description", "LanguageId", "Name" },
                values: new object[,]
                {
                    { 1, "C# byte", 1, "byte" },
                    { 2, "C# short", 1, "short" },
                    { 3, "C# int", 1, "int" },
                    { 4, "C# long", 1, "long" },
                    { 5, "C# bool", 1, "bool" },
                    { 6, "C# decimal", 1, "decimal" },
                    { 7, "C# float", 1, "float" },
                    { 8, "C# DateTime", 1, "DateTime" },
                    { 9, "C# Time", 1, "Time" },
                    { 10, "C# DateTimeOffset", 1, "DateTimeOffset" },
                    { 11, "C# string", 1, "string" },
                    { 12, "Python int", 2, "int" },
                    { 13, "Python bool", 2, "bool" },
                    { 14, "Python Decimal", 2, "Decimal" },
                    { 15, "Python Float", 2, "Float" },
                    { 16, "Python datetime", 2, "datetime" },
                    { 17, "Python str", 2, "str" },
                    { 18, "JavaScript number", 3, "number" },
                    { 19, "JavaScript BigInt", 3, "BigInt" },
                    { 20, "JavaScript boolean", 3, "boolean" },
                    { 21, "JavaScript Date", 3, "Date" },
                    { 22, "JavaScript string", 3, "string" }
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

            migrationBuilder.InsertData(
                table: "LanguageTypeSqlTypes",
                columns: new[] { "LanguageTypeId", "SqlTypeId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 24 },
                    { 3, 25 },
                    { 4, 4 },
                    { 5, 5 },
                    { 5, 26 },
                    { 6, 6 },
                    { 6, 7 },
                    { 6, 8 },
                    { 6, 9 },
                    { 7, 11 },
                    { 8, 12 },
                    { 8, 14 },
                    { 8, 16 },
                    { 8, 17 },
                    { 9, 13 },
                    { 10, 15 },
                    { 11, 18 },
                    { 11, 19 },
                    { 11, 20 },
                    { 11, 21 },
                    { 11, 22 },
                    { 11, 23 },
                    { 12, 1 },
                    { 12, 2 },
                    { 12, 3 },
                    { 12, 4 },
                    { 12, 24 },
                    { 12, 25 },
                    { 13, 5 },
                    { 13, 26 },
                    { 14, 6 },
                    { 14, 7 },
                    { 14, 8 },
                    { 14, 9 },
                    { 15, 10 },
                    { 15, 11 },
                    { 16, 12 },
                    { 16, 14 },
                    { 16, 15 },
                    { 16, 16 },
                    { 16, 17 },
                    { 17, 18 },
                    { 17, 19 },
                    { 17, 20 },
                    { 17, 21 },
                    { 17, 22 },
                    { 17, 23 },
                    { 18, 1 },
                    { 18, 2 },
                    { 18, 3 },
                    { 18, 6 },
                    { 18, 7 },
                    { 18, 8 },
                    { 18, 9 },
                    { 18, 10 },
                    { 18, 11 },
                    { 18, 24 },
                    { 18, 25 },
                    { 19, 4 },
                    { 20, 5 },
                    { 20, 26 },
                    { 21, 12 },
                    { 21, 14 },
                    { 21, 16 },
                    { 21, 17 },
                    { 22, 13 },
                    { 22, 15 },
                    { 22, 18 },
                    { 22, 19 },
                    { 22, 20 },
                    { 22, 21 },
                    { 22, 22 },
                    { 22, 23 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnProperties_PropertyId",
                table: "ColumnProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Columns_SqlTypeId",
                table: "Columns",
                column: "SqlTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBaseIndexTypes_IndexTypeId",
                table: "DataBaseIndexTypes",
                column: "IndexTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DataBaseTypes_SqlTypeId",
                table: "DataBaseTypes",
                column: "SqlTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IndexColumns_IndexId",
                table: "IndexColumns",
                column: "IndexId");

            migrationBuilder.CreateIndex(
                name: "IX_Indices_IndexTypeId",
                table: "Indices",
                column: "IndexTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageOrms_OrmId",
                table: "LanguageOrms",
                column: "OrmId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypes_LanguageId",
                table: "LanguageTypes",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageTypeSqlTypes_SqlTypeId",
                table: "LanguageTypeSqlTypes",
                column: "SqlTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DataBaseId",
                table: "Projects",
                column: "DataBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTables_TableId",
                table: "ProjectTables",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_OnDeleteId",
                table: "Relations",
                column: "OnDeleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_OnUpdateId",
                table: "Relations",
                column: "OnUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_SourceColumnId",
                table: "Relations",
                column: "SourceColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_Relations_TargetColumnId",
                table: "Relations",
                column: "TargetColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_TableColumns_TableId",
                table: "TableColumns",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_UserId",
                table: "UserProjects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Architectures");

            migrationBuilder.DropTable(
                name: "ColumnProperties");

            migrationBuilder.DropTable(
                name: "DataBaseIndexTypes");

            migrationBuilder.DropTable(
                name: "DataBaseTypes");

            migrationBuilder.DropTable(
                name: "IndexColumns");

            migrationBuilder.DropTable(
                name: "LanguageOrms");

            migrationBuilder.DropTable(
                name: "LanguageTypeSqlTypes");

            migrationBuilder.DropTable(
                name: "ProjectTables");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "TableColumns");

            migrationBuilder.DropTable(
                name: "UserProjects");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Indices");

            migrationBuilder.DropTable(
                name: "Orms");

            migrationBuilder.DropTable(
                name: "LanguageTypes");

            migrationBuilder.DropTable(
                name: "RelationActions");

            migrationBuilder.DropTable(
                name: "Columns");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "IndexTypes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "SqlTypes");

            migrationBuilder.DropTable(
                name: "DataBases");
        }
    }
}
