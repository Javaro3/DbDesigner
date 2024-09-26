using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum RoleEnum
{
    [Name("Administrator")]
    [Description("")]
    Admin = 1,
    
    [Name("User")]
    [Description("")]
    User = 2
}