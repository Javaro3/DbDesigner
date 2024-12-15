using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum LanguageTypeEnum
{
    [Name("byte")]
    [Description("C# byte")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.TinyInt)]
    ByteCs = 1,
    
    [Name("short")]
    [Description("C# short")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.TinyInt)]
    ShortCs = 2,
    
    [Name("int")]
    [Description("C# int")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.TinyInt, SqlTypeEnum.Integer, SqlTypeEnum.MediumInt, SqlTypeEnum.Int)]
    IntCs = 3,
    
    [Name("long")]
    [Description("C# long")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.BigInt)]
    LongCs = 4,
    
    [Name("bool")]
    [Description("C# bool")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Bit, SqlTypeEnum.Boolean)]
    BoolCs = 5,
    
    [Name("decimal")]
    [Description("C# decimal")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Decimal, SqlTypeEnum.Money, SqlTypeEnum.SmallMoney, SqlTypeEnum.Numeric)]
    DecimalCs = 6,
    
    [Name("float")]
    [Description("C# float")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Real)]
    FloatCs = 7,
    
    [Name("DateTime")]
    [Description("C# DateTime")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Date, SqlTypeEnum.DateTime2, SqlTypeEnum.DateTime, SqlTypeEnum.SmallDateTime)]
    DateTimeCs = 8,
    
    [Name("Time")]
    [Description("C# Time")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Time)]
    TimeCs = 9,
    
    [Name("DateTimeOffset")]
    [Description("C# DateTimeOffset")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.DateTimeOffset)]
    DateTimeOffsetCs = 10,
    
    [Name("string")]
    [Description("C# string")]
    [Language(LanguageEnum.CSharp)]
    [SqlType(SqlTypeEnum.Char, SqlTypeEnum.VarChar, SqlTypeEnum.Text, SqlTypeEnum.NChar, SqlTypeEnum.NVarChar, SqlTypeEnum.NText)]
    StringCs = 11,
    
    [Name("int")]
    [Description("Python int")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.TinyInt, SqlTypeEnum.SmallInt, SqlTypeEnum.Int, SqlTypeEnum.BigInt, SqlTypeEnum.Integer, SqlTypeEnum.MediumInt)]
    IntPython = 12,
    
    [Name("bool")]
    [Description("Python bool")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.Bit, SqlTypeEnum.Boolean)]
    BoolPython = 13,
    
    [Name("Decimal")]
    [Description("Python Decimal")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.Decimal, SqlTypeEnum.Numeric, SqlTypeEnum.Money, SqlTypeEnum.SmallMoney)]
    DecimalPython = 14,
    
    [Name("Float")]
    [Description("Python Float")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.Float, SqlTypeEnum.Real)]
    FloatPython = 15,
    
    [Name("datetime")]
    [Description("Python datetime")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.Date, SqlTypeEnum.DateTime, SqlTypeEnum.DateTime2, SqlTypeEnum.DateTimeOffset, SqlTypeEnum.SmallDateTime)]
    DateTimePython = 16,
    
    [Name("str")]
    [Description("Python str")]
    [Language(LanguageEnum.Python)]
    [SqlType(SqlTypeEnum.Char, SqlTypeEnum.VarChar, SqlTypeEnum.Text, SqlTypeEnum.NChar, SqlTypeEnum.NVarChar, SqlTypeEnum.NText)]
    StrPython = 17,
    
    [Name("number")]
    [Description("JavaScript number")]
    [Language(LanguageEnum.JavaScript)]
    [SqlType(SqlTypeEnum.TinyInt, SqlTypeEnum.SmallInt, SqlTypeEnum.Int, SqlTypeEnum.Decimal, SqlTypeEnum.Numeric, SqlTypeEnum.Money, SqlTypeEnum.SmallMoney, SqlTypeEnum.Float, SqlTypeEnum.Real, SqlTypeEnum.Integer, SqlTypeEnum.MediumInt)]
    NumberJs = 18,
    
    [Name("BigInt")]
    [Description("JavaScript BigInt")]
    [Language(LanguageEnum.JavaScript)]
    [SqlType(SqlTypeEnum.BigInt)]
    BigIntJs = 19,
    
    [Name("boolean")]
    [Description("JavaScript boolean")]
    [Language(LanguageEnum.JavaScript)]
    [SqlType(SqlTypeEnum.Bit, SqlTypeEnum.Boolean)]
    BooleanJs = 20,
    
    [Name("Date")]
    [Description("JavaScript Date")]
    [Language(LanguageEnum.JavaScript)]
    [SqlType(SqlTypeEnum.Date, SqlTypeEnum.DateTime2, SqlTypeEnum.DateTime, SqlTypeEnum.SmallDateTime)]
    DateJs = 21,
    
    [Name("string")]
    [Description("JavaScript string")]
    [Language(LanguageEnum.JavaScript)]
    [SqlType(SqlTypeEnum.Time, SqlTypeEnum.DateTimeOffset, SqlTypeEnum.Char, SqlTypeEnum.VarChar, SqlTypeEnum.Text, SqlTypeEnum.NChar, SqlTypeEnum.NVarChar, SqlTypeEnum.NText)]
    StringJs = 22,
}