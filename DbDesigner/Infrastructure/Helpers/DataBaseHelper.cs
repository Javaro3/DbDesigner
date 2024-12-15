using Common.Domain;
using Common.Dtos.DataBase;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class DataBaseHelper : IBaseHelper<DataBase, DataBaseFilterDto>
{
    public IQueryable<DataBase> ApplySort(IQueryable<DataBase> query, DataBaseFilterDto filter)
    {
        return query;
    }

    public IQueryable<DataBase> ApplyFilter(IQueryable<DataBase> query, DataBaseFilterDto filter)
    {
        if (!string.IsNullOrEmpty(filter.Description))
        {
            query = query.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));
        }
        
        if (filter.IndexTypes.Any())
        {
            query = query.Where(x => filter.IndexTypes.All(e => x.IndexTypes.Select(d => d.Id).Contains(e)));    
        }
        
        return query;
    }
}