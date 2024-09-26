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
    
    public static bool GetParams(this Enum value) 
    {
        var attributes = GetAttribute<ParamAttribute>(value);
        return attributes.Length > 0 && attributes[0].HasParam;
    }

    private static T[] GetAttribute<T>(Enum value) where T : Attribute
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (T[])fi.GetCustomAttributes(typeof(T), false);
        return attributes;
    }
}