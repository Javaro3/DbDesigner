using System.ComponentModel;
using Common.Attributes;
using Common.Enums;

namespace Common.Extensions;

public static class EnumExtension
{
    public static string GetName(this Enum value)
    {
        var attributes = GetAttribute<NameAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    
    public static string GetImage(this Enum value)
    {
        var attributes = GetAttribute<ImageAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Image : value.ToString();
    }
    
    public static string GetDescription(this Enum value) 
    {
        var attributes = GetAttribute<DescriptionAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
    
    public static IEnumerable<LanguageEnum> GetLanguages(this Enum value) 
    {
        var attributes = GetAttribute<LanguageAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Languages : [];
    }
    
    public static IEnumerable<DataBaseEnum> GetDataBases(this Enum value) 
    {
        var attributes = GetAttribute<DataBaseAttribute>(value);
        return attributes.Length > 0 ? attributes[0].DataBases : [];
    }
    
    public static IEnumerable<SqlTypeEnum> GetSqlTypes(this Enum value) 
    {
        var attributes = GetAttribute<SqlTypeAttribute>(value);
        return attributes.Length > 0 ? attributes[0].SqlTypes : [];
    }
    
    public static bool GetParams(this Enum value) 
    {
        var attributes = GetAttribute<HasParamsAttribute>(value);
        return attributes.Length > 0 && attributes[0].HasParams;
    }

    private static T[] GetAttribute<T>(Enum value) where T : Attribute
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (T[])fi.GetCustomAttributes(typeof(T), false);
        return attributes;
    }
}