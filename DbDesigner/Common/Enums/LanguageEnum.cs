using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum LanguageEnum
{
    [Name("C#")]
    [Description("An object-oriented programming language from Microsoft, widely used for developing enterprise applications, games and web services.")]
    [Image("cs.png")]
    CSharp = 1,
    
    [Name("Python")]
    [Description("A high-level programming language with a simple syntactic structure, popular for development, automation, data analysis and machine learning.")]
    [Image("py.png")]
    Python = 2,
    
    [Name("JavaScript")]
    [Description("A scripting language for developing interactive web pages, also actively used in server and mobile applications.")]
    [Image("js.png")]
    JavaScript = 3
}