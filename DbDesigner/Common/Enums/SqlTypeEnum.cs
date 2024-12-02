using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum SqlTypeEnum
{
    [Name("tinyint")]
    [Description("number from 0 to 255")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    TinyInt = 1,

    [Name("smallint")]
    [Description("number from 0 to 65 535")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite, DataBaseEnum.PostgreSql)]
    SmallInt = 2,

    [Name("int")]
    [Description("number from 0 to 2 147 483 647")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    Int = 3,

    [Name("bigint")]
    [Description("number from 0 to 2E64-1")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    BigInt = 4,

    [Name("bit")]
    [Description("number from 0 to 1")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql)]
    Bit = 5,

    [Name("decimal")]
    [Description("Numbers with fixed precision and scale. If maximum precision is used, valid values are in the -10^38 + 1 range 10^38 - 1.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql)]
    Decimal = 6,
    
    [Name("numeric")]
    [Description("Numbers with fixed precision and scale. If maximum precision is used, valid values are in the -10^38 + 1 range 10^38 - 1.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.SqLite, DataBaseEnum.PostgreSql)]
    Numeric = 7,

    [Name("money")]
    [Description("number from –922,337,203,685,477.5808 to 922,337,203,685,477.5807")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.PostgreSql)]
    Money = 8,
    
    [Name("smallmoney")]
    [Description("number from -214 748,3648 to 214 748,3647")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    SmallMoney = 9,

    [Name("float")]
    [Description("number from -1,79E+308 to 1,79E+308")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    Float = 10,

    [Name("real")]
    [Description("number from -1,79E+308 to 1,79E+308")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    Real = 11,

    [Name("date")]
    [Description("date type")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    Date = 12,

    [Name("time")]
    [Description("time type")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql)]
    Time = 13,

    [Name("datetime2")]
    [Description("Specifies a date that includes the time of day in 24-hour format")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    DateTime2 = 14,

    [Name("datetimeoffset")]
    [Description("Defines a date that is combined with the time of day and adds time zone awareness based on Coordinated Universal Time (UTC).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    DateTimeOffset = 15,

    [Name("datetime")]
    [Description("Avoid using date and time for new work. Use date, date, datetime2, and datetimeoffset data types instead.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    DateTime = 16,

    [Name("smalldatetime")]
    [Description("Specifies a date that matches the time of day. Time is presented in 24-hour format with seconds always set to zero (:00), and no fractional seconds.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    SmallDateTime = 17,

    [Name("char")]
    [Description("Fixed-size string data. n specifies the size of the string in bytes and must be a value between 1 and 8000")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql)]
    Char = 18,

    [Name("varchar")]
    [Description("Fixed-size string data. n specifies the size of the string in bytes and must be a value between 1 and 8000")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    VarChar = 19,

    [Name("text")]
    [Description("This is a fixed and variable length data type designed to store character and binary data in Unicode format")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    Text = 20,

    [Name("nchar")]
    [Description("This is a fixed and variable length data type designed to store character and binary data in Unicode format")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    NChar = 21,

    [Name("nvarchar")]
    [Description("This is a fixed and variable length data type designed to store character and binary data in Unicode format")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.PostgreSql, DataBaseEnum.SqLite)]
    NVarChar = 22,

    [Name("ntext")]
    [Description("This is a fixed and variable length data type designed to store character and binary data in Unicode format")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.Mssql)]
    NText = 23,

    [Name("integer")]
    [Description("signed four-byte integer")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite, DataBaseEnum.PostgreSql, DataBaseEnum.MySql)]
    Integer = 24,

    [Name("mediumint")]
    [Description("A medium integer. Signed range is from -8388608 to 8388607.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite, DataBaseEnum.MySql)]
    MediumInt = 25,

    [Name("boolean")]
    [Description("logical Boolean (true/false)")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite, DataBaseEnum.MySql, DataBaseEnum.PostgreSql)]
    Boolean = 26
}