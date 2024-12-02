using Common.Domain;
using Common.Dtos.User;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class UserHelper : IBaseHelper<User, UserFilterDto>
{
    public IQueryable<User> ApplySort(IQueryable<User> query, UserFilterDto filter)
    {
        return query;
    }

    public IQueryable<User> ApplyFilter(IQueryable<User> query, UserFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Email))
        {
            query = query.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
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
        
        if (filter.Roles.Any())
        {
            query = query.Where(x => filter.Roles.All(e => x.Roles.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}