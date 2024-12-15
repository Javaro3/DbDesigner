using Common.Dtos.DataBase;
using Common.Dtos.Relation;
using Common.Dtos.Table;

namespace Common.Dtos.Project;

public class ProjectDiagramDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DataBaseDto? DataBase { get; set; }

    public ICollection<TableDiagramDto> Tables { get; set; } = [];

    public ICollection<RelationDto> Relations { get; set; } = [];
}