using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum RoleEnum
{
    [Name("Administrator")]
    [Description("")]
    Admin = 1,
    
    [Name("User")]
    [Description("")]
    User = 2
}