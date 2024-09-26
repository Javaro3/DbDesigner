using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum OrmEnum
{
    [Name("Entity Framework")]
    [Description("An ORM for C# and .NET that provides a high level of abstraction for working with databases, allowing you to use LINQ for queries.")]
    [Language(LanguageEnum.CSharp)]
    EntityFramework = 1,
    
    [Name("Dapper")]
    [Description("A lightweight mixin for C# that provides high-performance access to databases with minimal wrapper around SQL queries.")]
    [Language(LanguageEnum.CSharp)]
    Dapper = 2,
    
    [Name("NHibernate")]
    [Description("A powerful ORM for C# with extensive customization capabilities for mapping objects to relational databases.")]
    [Language(LanguageEnum.CSharp)]
    NHibernate = 3,
    
    [Name("SQLAlchemy")]
    [Description("A popular ORM for Python that supports powerful mapping and database schema management.")]
    [Language(LanguageEnum.Python)]
    SqlAlchemy = 4,
    
    [Name("Django ORM")]
    [Description("A built-in ORM in Django that provides a high-level abstraction for working with databases in Python web applications.")]
    [Language(LanguageEnum.Python)]
    DjangoOrm = 5,
    
    [Name("Tortoise ORM")]
    [Description("An asynchronous ORM for Python designed to work with databases using Django ORM-like syntax.")]
    [Language(LanguageEnum.Python)]
    TortoiseOrm = 6,
    
    [Name("Peewee")]
    [Description("A lightweight ORM for Python that offers a simple and intuitive model for working with databases.")]
    [Language(LanguageEnum.Python)]
    Peewee = 7,
    
    [Name("Sequelize")]
    [Description("ORM for Node.js with support for various relational databases and a powerful interface for working with SQL queries.")]
    [Language(LanguageEnum.JavaScript)]
    Sequelize = 8,
    
    [Name("TypeORM")]
    [Description("ORM for TypeScript and JavaScript, providing support for TypeScript annotations and work with multiple relational databases.")]
    [Language(LanguageEnum.JavaScript)]
    TypeOrm = 9
}