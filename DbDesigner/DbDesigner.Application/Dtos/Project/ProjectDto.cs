using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Dtos.User;

namespace DbDesigner.Application.Dtos.Project;

public class ProjectDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DataBaseDto? DataBase { get; set; }
    
    public UserDto? User { get; set; }
}