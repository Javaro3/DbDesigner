using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum SqlTypeEnum
{
    #region SqlLite

    [Name("INTEGER")]
    [Description("Stores integer values. Flexible storage (1, 2, 3, 4, 6, or 8 bytes depending on magnitude).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteInteger = 1,
    
    [Name("REAL")]
    [Description("Stores floating-point numbers (8-byte IEEE double precision).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteReal = 2,
    
    [Name("TEXT")]
    [Description("Stores UTF-8/UTF-16 encoded string values.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteText = 3,
    
    [Name("NUMERIC")]
    [Description("Stores numeric values (integer or floating-point). Behaves like INTEGER/REAL based on context.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteNumeric = 4,

    #endregion

    #region MySql

    [Name("INT")]
    [Description("4-byte signed integer. Range: -2,147,483,648 to 2,147,483,647.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlInt = 5,

    [Name("TINYINT")]
    [Description("1-byte signed integer. Range: -128 to 127. UNSIGNED: 0 to 255.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlTinyInt = 6,
    
    [Name("SMALLINT")]
    [Description("2-byte signed integer. Range: -32,768 to 32,767. UNSIGNED: 0 to 65,535.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlSmallInt = 7,
    
    [Name("MEDIUMINT")]
    [Description("3-byte signed integer. Range: -8,388,608 to 8,388,607. UNSIGNED: 0 to 16,777,215.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlMediumInt = 8,
    
    [Name("BIGINT")]
    [Description("8-byte signed integer. Range: -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlBigInt = 9,

    [Name("FLOAT")]
    [Description("4-byte floating-point number (single precision). Approximate precision: 7 decimal digits.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlFloat = 10,
    
    [Name("DOUBLE")]
    [Description("8-byte floating-point number (double precision). Approximate precision: 15 decimal digits.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlDouble = 11,
    
    [Name("DECIMAL")]
    [Description("Fixed-point number with exact precision. Syntax: DECIMAL(M,D) where M=total digits, D=decimal digits.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlDecimal = 12,
    
    [Name("CHAR")]
    [Description("Fixed-length string. Padded with spaces to specified length. Max length: 255 characters.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlChar = 13,
    
    [Name("VARCHAR")]
    [Description("Variable-length string. Max length: 65,535 characters.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlVarChar = 14,
    
    [Name("TEXT")]
    [Description("Variable-length string for large text. Max size: 65,535 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlText = 15,
    
    [Name("TINYTEXT")]
    [Description("Variable-length string. Max size: 255 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlTinyText = 16,

    [Name("MEDIUMTEXT")]
    [Description("Variable-length string. Max size: 16,777,215 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlMediumText = 17,
    
    [Name("LONGTEXT")]
    [Description("Variable-length string. Max size: 4,294,967,295 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlLongText = 18,
    
    [Name("BINARY")]
    [Description("Fixed-length binary data. Padded with zeros. Max length: 255 bytes.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlBinary = 19,

    [Name("VARBINARY")]
    [Description("Variable-length binary data. Max length: 65,535 bytes.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlVarBinary = 20,
    
    [Name("BLOB")]
    [Description("Binary Large Object. Max size: 65,535 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlBlob = 21,
    
    [Name("TINYBLOB")]
    [Description("Binary Large Object. Max size: 255 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlTinyBlob = 22,
    
    [Name("MEDIUMBLOB")]
    [Description("Binary Large Object. Max size: 16,777,215 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlMediumBlob = 23,
    
    [Name("LONGBLOB")]
    [Description("Binary Large Object. Max size: 4,294,967,295 bytes.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlLongBlob = 24,
    
    [Name("DATE")]
    [Description("Stores date in 'YYYY-MM-DD' format. Range: '1000-01-01' to '9999-12-31'.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlDate = 25,
    
    [Name("TIME")]
    [Description("Stores time in 'HH:MM:SS' format. Range: '-838:59:59' to '838:59:59'.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlTime = 26,
    
    [Name("DATETIME")]
    [Description("Stores date and time in 'YYYY-MM-DD HH:MM:SS' format. Range: '1000-01-01 00:00:00' to '9999-12-31 23:59:59'.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlDateTime = 27,
    
    [Name("TIMESTAMP")]
    [Description("Stores UTC timestamp. Range: '1970-01-01 00:00:01' UTC to '2038-01-19 03:14:07' UTC.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MySql)]
    MySqlTimeSpan = 28,

    #endregion

    #region PostgreSql

    [Name("INT")]
    [Description("4-byte signed integer. Alias for INTEGER. Range: -2,147,483,648 to 2,147,483,647.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlInt = 29,
    
    [Name("SMALLINT")]
    [Description("2-byte signed integer. Range: -32,768 to 32,767.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlSmallInt = 30,
    
    [Name("BIGINT")]
    [Description("8-byte signed integer. Range: -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlBigInt = 31,
    
    [Name("DECIMAL")]
    [Description("Exact numeric type. Syntax: DECIMAL(precision, scale). Precision: up to 131072 digits.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlDecimal = 32,
    
    [Name("NUMERIC")]
    [Description("Alias for DECIMAL. Exact numeric type with configurable precision and scale.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlNumeric = 33,
    
    [Name("REAL")]
    [Description("4-byte floating-point number (single precision). Approximate precision: 6 decimal digits.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlReal = 34,
    
    [Name("CHAR")]
    [Description("Fixed-length string. Padded with spaces. Syntax: CHAR(n). Max length: 1GB.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlChar = 35,

    [Name("VARCHAR")]
    [Description("Variable-length string. Syntax: VARCHAR(n). Max length: 1GB.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlVarChar = 36,
    
    [Name("TEXT")]
    [Description("Variable-length string with unlimited length (up to 1GB).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlText = 37,
    
    [Name("BYTEA")]
    [Description("Binary data (equivalent to BLOB in other databases).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlBytea = 38,
    
    [Name("DATE")]
    [Description("Stores date in 'YYYY-MM-DD' format. Range: 4713 BC to 294276 AD.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlDate = 39,
    
    [Name("TIME")]
    [Description("Stores time in 'HH:MM:SS' format. Includes optional time zone.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlTime = 40,
    
    [Name("TIMESTAMP")]
    [Description("Stores date and time (without time zone) in 'YYYY-MM-DD HH:MM:SS' format.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlTimeSpan = 41,
    
    [Name("BOOLEAN")]
    [Description("Stores true/false values. Valid inputs: TRUE, FALSE, NULL.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlBoolean = 42,

    #endregion
    
    #region MsSql

    [Name("TINYINT")]
    [Description("1-byte integer storing whole numbers from 0 to 255.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlTinyInt = 43,

    [Name("SMALLINT")]
    [Description("2-byte integer storing whole numbers from -32,768 to 32,767.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlSmallInt = 44,

    [Name("INT")]
    [Description("4-byte integer storing whole numbers from -2,147,483,648 to 2,147,483,647.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlInt = 45,

    [Name("BIGINT")]
    [Description("8-byte integer storing whole numbers from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlBigInt = 46,
    
    [Name("DECIMAL")]
    [Description("Fixed precision and scale numeric data with user-defined precision (max 38 digits).")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlDecimal = 49,

    [Name("NUMERIC")]
    [Description("Functionally equivalent to DECIMAL - fixed precision numeric data.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNumeric = 50,
    
    [Name("MONEY")]
    [Description("8-byte currency value from -922,337,203,685,477.5808 to 922,337,203,685,477.5807.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlMoney = 51,

    [Name("SMALLMONEY")]
    [Description("4-byte currency value from -214,748.3648 to 214,748.3647.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlSmallMoney = 52,
    
    [Name("CHAR")]
    [Description("Fixed-length non-Unicode character data (max 8,000 chars).")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlChar = 53,

    [Name("NCHAR")]
    [Description("Fixed-length Unicode character data (max 4,000 chars).")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNChar = 54,

    [Name("VARCHAR")]
    [Description("Variable-length non-Unicode character data (max 8,000 chars, or MAX for 2GB).")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlVarChar = 55,

    [Name("NVARCHAR")]
    [Description("Variable-length Unicode character data (max 4,000 chars, or MAX for 1GB).")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNVarChar = 56,

    [Name("TEXT")]
    [Description("Legacy variable-length non-Unicode data (deprecated, use VARCHAR(MAX) instead).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlText = 57,

    [Name("NTEXT")]
    [Description("Legacy variable-length Unicode data (deprecated, use NVARCHAR(MAX) instead).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNText = 58,
    
    [Name("DATE")]
    [Description("Date only (0001-01-01 through 9999-12-31).")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlDate = 59,

    [Name("TIME")]
    [Description("Time only with user-defined fractional second precision.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlTime = 60,

    [Name("DATETIME")]
    [Description("Date and time (1753-01-01 through 9999-12-31) with 3.33ms accuracy.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlDateTime = 61,

    [Name("DATETIME2")]
    [Description("Date and time with higher precision (0001-01-01 through 9999-12-31) and user-defined fractional seconds.")]
    [HasParams(true)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlDateTime2 = 62,

    [Name("SMALLDATETIME")]
    [Description("Compact date and time (1900-01-01 through 2079-06-06) with 1-minute accuracy.")]
    [HasParams(false)]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlSmallDateTime = 63
    
    #endregion
}