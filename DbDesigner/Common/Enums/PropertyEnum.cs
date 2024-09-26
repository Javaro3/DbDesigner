using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum PropertyEnum
{
    [Name("NOT NULL")]
    [Description("Prevents a column from being NULL.")]
    [Param(false)]
    NotNull = 1,
    
    [Name("DEFAULT")]
    [Description("Sets the default value for the column if no value is specified during data insertion.")]
    [Param(true)]
    Default = 2,
    
    [Name("PRIMARY KEY")]
    [Description("Defines the column as a primary key, which must be unique for each row and not contain NULLs.")]
    [Param(false)]
    PrimaryKey = 3,
    
    [Name("INCREMENT")]
    [Description("Automatically increments the column value with each new entry. Typically used with primary keys.")]
    [Param(false)]
    Increment = 4,
    
    [Name("UNIQUE")]
    [Description("Ensures that the values in a column are unique. You cannot insert duplicate values.")]
    [Param(false)]
    Unique = 5,
    
    [Name("CHECK")]
    [Description("Defines a condition that column values must meet.")]
    [Param(true)]
    Check = 6
}