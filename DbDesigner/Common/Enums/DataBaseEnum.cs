using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum DataBaseEnum
{
    [Name("MSSQL")]
    [Description("A commercial DBMS from Microsoft with deep integration into the Windows ecosystem and powerful analytical capabilities.")]
    Mssql = 1,

    [Name("PostgreSQL")]
    [Description("An open object-relational DBMS with support for complex queries, extensions and highly reliable transactions.")]
    PostgreSql = 2,

    [Name("MySQL")]
    [Description("A popular and fast open DBMS, widely used in web applications due to its ease of configuration and cross-platform functionality.")]
    MySql = 3,

    [Name("SQLite")]
    [Description("A lightweight embedded DBMS that does not require a server, ideal for mobile applications and prototyping.")]
    SqLite = 4
}