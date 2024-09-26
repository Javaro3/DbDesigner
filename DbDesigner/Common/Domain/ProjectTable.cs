using Common.Domain.BaseDomain;

namespace Common.Domain;

public class ProjectTable : BaseModel
{
    public int ProjectId { get; set; }

    public int TableId { get; set; } 
}