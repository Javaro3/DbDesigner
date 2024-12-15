using Common.Domain;
using Common.Dtos.Role;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class RoleHelper : IBaseHelper<Role, RoleFilterDto>
{
    public IQueryable<Role> ApplySort(IQueryable<Role> query, RoleFilterDto filter)
    {
        return query;
    }

    public IQueryable<Role> ApplyFilter(IQueryable<Role> query, RoleFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        return query;
    }
}