using System.ComponentModel;
using DbDesigner.Domain.Attributes;
using DbDesigner.Domain.Enums;

namespace DbDesigner.Domain.Extensions;

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

    public static bool GetParams(this Enum value) 
    {
        var attributes = GetAttribute<HasParamsAttribute>(value);
        return attributes.Length > 0 && attributes[0].HasParams;
    }

    public static string GetImage(this Enum value)
    {
        var attributes = GetAttribute<ImageAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Image : value.ToString();
    }

    public static LanguageEnum GetLanguage(this Enum value) 
    {
        var attributes = GetAttribute<LanguageAttribute>(value);
        return attributes.Length > 0 ? attributes[0].Language : default;
    }
    
    public static DataBaseEnum GetDataBase(this Enum value) 
    {
        var attributes = GetAttribute<DataBaseAttribute>(value);
        return attributes.Length > 0 ? attributes[0].DataBase : default;
    }

    private static T[] GetAttribute<T>(Enum value) where T : Attribute
    {
        var fi = value.GetType().GetField(value.ToString());
        var attributes = (T[])fi.GetCustomAttributes(typeof(T), false);
        return attributes;
    }
}