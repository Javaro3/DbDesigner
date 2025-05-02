namespace DbDesigner.Application.Dtos.Generate;

public class DalGeneratorRequestDto
{
    public int ProjectId { get; set; }

    public int LanguageId { get; set; }
    
    public int DataBaseId { get; set; }

    public int ArchitectureId { get; set; }

    public int OrmId { get; set; }
    
    public int GenerationLanguageId { get; set; }

    public int GenerationModelId { get; set; }
    
    public bool GenerateTestData { get; set; }

    public bool GenerateDal { get; set; }

    public List<TableGeneratorRequestDto> TableGenerateInfos { get; set; } = [];
}