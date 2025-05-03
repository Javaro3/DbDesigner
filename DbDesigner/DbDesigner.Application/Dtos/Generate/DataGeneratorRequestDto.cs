using DbDesigner.Application.Dtos.Project;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Extensions;

namespace DbDesigner.Application.Dtos.Generate;

public class DataGeneratorRequestDto
{
    public ProjectDiagramDto? Project { get; set; }
    
    public int GenerationModelId { get; set; }

    public string GenerationLanguage { get; set; } = GenerationLanguageEnum.English.GetName();
    
    public ICollection<TableGeneratorRequestDto> TableGenerateInfos { get; set; } = [];
}