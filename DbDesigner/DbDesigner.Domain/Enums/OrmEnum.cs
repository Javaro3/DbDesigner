using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum OrmEnum
{
    [Name("Entity Framework")]
    [Description("An ORM for C# and .NET that provides a high level of abstraction for working with databases, allowing you to use LINQ for queries.")]
    [Language(LanguageEnum.CSharp)]
    EntityFramework = 1,
    
    [Name("SQLAlchemy")]
    [Description("A popular ORM for Python that supports powerful mapping and database schema management.")]
    [Language(LanguageEnum.Python)]
    SqlAlchemy = 2,

    [Name("Sequelize")]
    [Description("ORM for Node.js with support for various relational databases and a powerful interface for working with SQL queries.")]
    [Language(LanguageEnum.JavaScript)]
    Sequelize = 3,
}