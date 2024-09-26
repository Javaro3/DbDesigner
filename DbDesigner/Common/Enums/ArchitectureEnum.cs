using System.ComponentModel;
using Common.Attributes;

namespace Common.Enums;

public enum ArchitectureEnum
{
    [Name("Direct Data Access (DDA)")]
    [Description("An approach in which code interacts directly with the database, without the use of abstraction layers.")]
    DirectDataAccess = 1,
    
    [Name("Repository Pattern")]
    [Description("A design pattern that provides an abstraction for working with data, allowing data access logic to be separated from business logic.")]
    RepositoryPattern = 2,
    
    [Name("Command Query Responsibility Segregation (CQRS)")]
    [Description("An architectural pattern that separates state change operations (commands) from data requests (reads), ensuring their independence.")]
    CommandQueryResponsibilitySegregation = 3
}