using DbDesigner.Application.Dtos.Generate;

namespace DbDesigner.Application.Dtos.Project;

public class ProjectGenerateDto
{
    public int ProjectId { get; set; }
    
    public int DataBaseId { get; set; }
    
    public int LanguageId { get; set; }
    
    public int OrmId { get; set; }
    
    public int ArchitectureId { get; set; }

    public ICollection<TableGeneratorRequestDto> TableGenerateInfos { get; set; } = [];
}