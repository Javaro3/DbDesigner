using Common.Dtos.DataBase;

namespace Common.Dtos.IndexType;

public class IndexTypeDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public ICollection<DataBaseDto> DataBases { get; set; } = [];
}