using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum PermissionEnum
{
    [Name("Create")]
    [Description("")]
    Create = 1,
    
    [Name("Read")]
    [Description("")]
    Read = 2,
    
    [Name("Update")]
    [Description("")]
    Update = 3, 
    
    [Name("Delete")]
    [Description("")]
    Delete = 4
}