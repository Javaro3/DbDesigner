using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum PropertyEnum
{
    #region SqlLite

    [Name("PRIMARY KEY")]
    [Description("Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLitePrimaryKey = 1,

    [Name("UNIQUE")]
    [Description("Ensures that all values in the column are unique. Prevents duplicate entries.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteUnique = 2,

    [Name("NOT NULL")]
    [Description("Ensures that the column cannot contain NULL values.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteNotNull = 3,

    [Name("DEFAULT")]
    [Description("Sets a default value for the column if no value is provided during insertion.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteDefault = 4,

    [Name("CHECK")]
    [Description("Enforces a condition that must be true for all values in the column.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteCheck = 5,

    #endregion
    
    #region MySql

    [Name("PRIMARY KEY")]
    [Description("Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlPrimaryKey = 6,

    [Name("AUTO_INCREMENT")]
    [Description("Automatically generates a unique value for the column, incrementing with each new row.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlAutoIncrement = 7,
    
    [Name("UNIQUE")]
    [Description("Ensures that all values in the column are unique. Prevents duplicate entries.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlUnique = 8,
    
    [Name("NOT NULL")]
    [Description("Ensures that the column cannot contain NULL values.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlNotNull = 9,
    
    [Name("DEFAULT")]
    [Description("Sets a default value for the column if no value is provided during insertion.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlDefault = 10,

    [Name("CHECK")]
    [Description("Enforces a condition that must be true for all values in the column.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlCheck = 11,
    
    [Name("UNSIGNED")]
    [Description("Restricts the column to non-negative numeric values.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlUnsigned = 12,
    
    #endregion
    
    #region PostgreSql

    [Name("PRIMARY KEY")]
    [Description("Uniquely identifies each row in the table. Automatically enforces uniqueness and creates an index.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlPrimaryKey = 13,

    [Name("GENERATED ALWAYS AS IDENTITY")]
    [Description("Automatically generates a unique value for the column, similar to auto-increment but compliant with SQL standards.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlGeneratedAsIdentity = 14,
    
    [Name("UNIQUE")]
    [Description("Ensures that all values in the column are unique. Prevents duplicate entries.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlUnique = 15,
    
    [Name("NOT NULL")]
    [Description("Ensures that the column cannot contain NULL values.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlNotNull = 16,
    
    [Name("DEFAULT")]
    [Description("Sets a default value for the column if no value is provided during insertion.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlDefault = 17,
    
    [Name("CHECK")]
    [Description("Enforces a condition that must be true for all values in the column.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlCheck = 18,

    #endregion
    
    #region MsSql

    [Name("NULL")]
    [Description("Allows the column to contain NULL values. NULL means the absence of data or unknown value.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNull = 19,
    
    [Name("NOT NULL")]
    [Description("Prohibits the column contain NULL values. The column must always matter.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNotNull = 20,
    
    [Name("IDENTITY")]
    [Description("Automatically generates unique increasing numbers for the column. Parameters: initial value and extension step.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlIdentity = 21,
    
    [Name("PRIMARY KEY")]
    [Description("The primary key uniquely identifies each line of the table. Automatically creates a cluster index.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlPrimaryKey = 22,
    
    [Name("UNIQUE")]
    [Description("Provides the uniqueness of the values in the column. Creates a non -lasterized index to verify uniqueness.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlUnique = 23,
    
    [Name("CHECK")]
    [Description("A restriction that checks the values of the column according to a given condition. Parameters: Logical expression for verification.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlCheck = 24,
    
    [Name("DEFAULT")]
    [Description("Sets the default value for the column when inserting a new line, if the value is not indicated clearly. Parameters: meaning or expression.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlDefault = 25

    #endregion
}