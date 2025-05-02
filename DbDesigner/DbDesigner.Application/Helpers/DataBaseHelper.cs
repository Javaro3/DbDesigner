using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;

namespace DbDesigner.Application.Helpers;

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
        
        return query;
    }
}