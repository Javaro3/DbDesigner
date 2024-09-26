using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum RelationActionEnum
{
    [Name("CASCADE")]
    [Description("When you delete or update a parent record, the associated child records are automatically deleted or updated.")]
    Cascade = 1,

    [Name("SET NULL")]
    [Description("When a parent record is deleted or updated, the corresponding foreign keys in the child records are set to NULL.")]
    SetNull = 2,
    
    [Name("SET DEFAULT")]
    [Description("When a parent record is deleted or updated, the foreign keys in the child records are set to the default value.")]
    SetDefault = 3,
    
    [Name("RESTRICT")]
    [Description("Prevents a parent record from being deleted or updated if there are associated child records.")]
    Restrict = 4,
    
    [Name("NO ACTION")]
    [Description("Similar to RESTRICT, but data integrity checks are performed later, after all transaction actions.")]
    NoAction = 5
}