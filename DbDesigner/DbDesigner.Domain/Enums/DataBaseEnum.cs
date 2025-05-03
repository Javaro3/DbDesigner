using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum DataBaseEnum
{
    [Name("PostgreSQL")]
    [Description("An open object-relational DBMS with support for complex queries, extensions and highly reliable transactions.")]
    [Image("PostgreSQL.png")]
    PostgreSql = 1,

    [Name("MySQL")]
    [Description("A popular and fast open DBMS, widely used in web applications due to its ease of configuration and cross-platform functionality.")]
    [Image("MySQL.png")]
    MySql = 2,

    [Name("SQLite")]
    [Description("A lightweight embedded DBMS that does not require a server, ideal for mobile applications and prototyping.")]
    [Image("SQLite.png")]
    SqLite = 3,
    
    [Name("MsSql")]
    [Description("A high-performance relational database by Microsoft with robust security, enterprise-grade features, and deep integration with Windows ecosystem and Azure cloud services.")]
    [Image("MsSql.png")]
    MsSql = 4
}