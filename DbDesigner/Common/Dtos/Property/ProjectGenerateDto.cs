using Common.Dtos.Table;

namespace Common.Dtos.Property;

public class ProjectGenerateDto
{
    public int ProjectId { get; set; }
    
    public int DataBaseId { get; set; }
    
    public int LanguageId { get; set; }
    
    public int OrmId { get; set; }
    
    public int ArchitectureId { get; set; }

    public ICollection<TableGenerateDto> TableGenerateInfos { get; set; } = [];
}