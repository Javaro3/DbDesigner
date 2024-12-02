using Common.Domain;
using Common.Dtos.Project;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class ProjectHelper : IBaseHelper<Project, ProjectFilterDto>
{
    public IQueryable<Project> ApplySort(IQueryable<Project> query, ProjectFilterDto filter)
    {
        return query;
    }

    public IQueryable<Project> ApplyFilter(IQueryable<Project> query, ProjectFilterDto filter)
    {
        if (filter.CurrentUser.HasValue)
        {
            query = query.Where(x => x.Users.Select(e => e.Id).Contains(filter.CurrentUser.Value));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }

        if (filter.CreatedOnFrom.HasValue)
        {
            var createdOnFrom = filter.CreatedOnFrom.Value.ToUniversalTime();
            query = query.Where(x => x.CreatedOn >= createdOnFrom);
        }
        
        if (filter.CreatedOnTo.HasValue)
        {
            var createdOnTo = filter.CreatedOnTo.Value.ToUniversalTime();
            query = query.Where(x => x.CreatedOn <= createdOnTo);
        }
        
        if (filter.Users.Any())
        {
            query = query.Where(x => filter.Users.All(e => x.Users.Select(d => d.Id).Contains(e)));    
        }
        
        if (filter.DataBases.Any())
        {
            query = query.Where(x => filter.DataBases.Contains(x.DataBaseId));   
        }
        
        return query;
    }
}