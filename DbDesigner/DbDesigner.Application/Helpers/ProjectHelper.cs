using DbDesigner.Application.Dtos.Project;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

public class ProjectHelper : IBaseHelper<Project, ProjectFilterDto>
{
    public IQueryable<Project> ApplySort(IQueryable<Project> query, ProjectFilterDto filter)
    {
        return query;
    }

    public IQueryable<Project> ApplyFilter(IQueryable<Project> query, ProjectFilterDto filter)
    {
        if (filter.User.HasValue)
        {
            query = query.Where(x => x.UserId == filter.User.Value);
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (filter.DataBases.Any())
        {
            query = query.Where(x => filter.DataBases.Contains(x.DataBaseId));   
        }
        
        return query;
    }
}