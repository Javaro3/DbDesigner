using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum IndexTypeEnum
{
    [Name("Clustered Index")]
    [Description("Table data is sorted and physically stored in the order specified by the index.")]
    [DataBase(DataBaseEnum.Mssql)]
    ClusteredIndex = 1,
    
    [Name("Non-Clustered Index")]
    [Description("Contains pointers to data, rather than the data itself.")]
    [DataBase(DataBaseEnum.Mssql)]
    NonClusteredIndex = 2,
    
    [Name("Unique Index")]
    [Description("Ensures that values in a column or set of columns are unique.")]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.PostgreSql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    UniqueIndex = 3,
    
    [Name("Full-Text Index")]
    [Description("Used to search text in large text data.")]
    [DataBase(DataBaseEnum.Mssql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    FullTextIndex = 4,
    
    [Name("B-tree Index")]
    [Description("The standard index for most search operations.")]
    [DataBase(DataBaseEnum.PostgreSql, DataBaseEnum.MySql, DataBaseEnum.SqLite)]
    BTreeIndex = 5,
    
    [Name("Hash Index")]
    [Description("Index for quick search by exact value.")]
    [DataBase(DataBaseEnum.PostgreSql, DataBaseEnum.MySql)]
    HashIndex = 6,
}