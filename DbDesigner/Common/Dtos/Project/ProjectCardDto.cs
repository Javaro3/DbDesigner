using Common.Dtos.DataBase;
using Common.Dtos.User;

namespace Common.Dtos.Project;

public class ProjectCardDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public DataBaseDto? DataBase { get; set; }
    
    public List<UserDto> Users { get; set; } = [];
}