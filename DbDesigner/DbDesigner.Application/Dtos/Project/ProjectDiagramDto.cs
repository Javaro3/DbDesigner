using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Application.Dtos.Table;

namespace DbDesigner.Application.Dtos.Project;

public class ProjectDiagramDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DataBaseDto? DataBase { get; set; }

    public ICollection<TableDto> Tables { get; set; } = [];

    public ICollection<RelationDto> Relations { get; set; } = [];
}