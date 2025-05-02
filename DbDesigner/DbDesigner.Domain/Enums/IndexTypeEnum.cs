using System.ComponentModel;
using DbDesigner.Domain.Attributes;

namespace DbDesigner.Domain.Enums;

public enum IndexTypeEnum
{
    #region SqlLite
    
    [Name("Index")]
    [Description("Standard index that improves query performance by allowing faster data retrieval. Data is logically ordered, but not physically stored in the index order.")]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteIndex = 1,
    
    [Name("Unique Index")]
    [Description("Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.")]
    [DataBase(DataBaseEnum.SqLite)]
    SqlLiteUniqueIndex = 2,

    #endregion
    
    #region MySql
    
    [Name("B-tree")]
    [Description("Default index type in MySQL. Suitable for most queries, including equality checks, range queries, and sorting. Data is stored in a balanced tree structure.")]
    [DataBase(DataBaseEnum.MySql)]
    MySqlBTreeIndex = 3,
    
    [Name("UNIQUE")]
    [Description("Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.")]
    [DataBase(DataBaseEnum.MySql)]
    MySqlUniqueIndex = 4,

    [Name("FULLTEXT")]
    [Description("Optimized for full-text search queries. Allows efficient searching of text data using natural language queries.")]
    [DataBase(DataBaseEnum.MySql)]
    MySqlFullTextIndex = 5,
    
    #endregion
    
    #region PostgreSql
    
    [Name("B-tree")]
    [Description("Default index type in PostgreSQL. Suitable for most queries, including equality checks, range queries, and sorting. Data is stored in a balanced tree structure.")]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlBTreeIndex = 6,
    
    [Name("HASH")]
    [Description("Optimized for equality checks (=). Provides fast lookups but does not support range queries or sorting. Suitable for exact match queries.")]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlHashIndex = 7,
    
    [Name("UNIQUE")]
    [Description("Ensures that all values in the indexed column(s) are unique. Improves query performance and enforces data integrity by preventing duplicate entries.")]
    [DataBase(DataBaseEnum.PostgreSql)]
    PostgreSqlUniqueIndex = 8,

    #endregion
    
    #region MsSql
    
    [Name("CLUSTERED")]
    [Description("Determines the physical order of data in the table. The table can have only one cluster index.")]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlClusteredIndex = 9,
    
    [Name("NONCLUSTERED")]
    [Description("A separate structure that stores a copy of the indexed columns with an indicator to the data. The table can have up to 999 non -lasterized indices.")]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlNonClusteredIndex = 10,
    

    [Name("UNIQUE NONCLUSTERED")]
    [Description("Guarantees the uniqueness of values in indexed columns.")]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlUniqueNonClusteredIndex = 11,
    
    [Name("UNIQUE CLUSTERED")]
    [Description("Guarantees the uniqueness of values in indexed columns.")]
    [DataBase(DataBaseEnum.MsSql)]
    MsSqlUniqueClusteredIndex = 12

    #endregion
}