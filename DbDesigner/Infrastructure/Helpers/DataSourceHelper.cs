using System.Linq.Expressions;
using AutoMapper;
using Common.Dtos;
using Common.Enums;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Infrastructure.Helpers;

public class DataSourceHelper : IDataSourceHelper
{
    public async Task<TransportDto<TDto>> ApplyDataSource<T, TDto>(IQueryable<T> query, FilterRequestDto filter, IMapper mapper)
    {
        if (filter.SortField != null)
        {
            var sortExpression = OrderByDynamic<T>(filter.SortField.Field);
            query = filter.SortField.Direction == SortDirection.Asc
                ? query.OrderBy(sortExpression)
                : query.OrderByDescending(sortExpression);
        }
        
        var totalCount = await query.CountAsync();
        query = query.Skip((filter.PageNumber-1) * filter.PageSize).Take(filter.PageSize);
        var dtos = mapper.Map<List<TDto>>(query);
        
        return new TransportDto<TDto> {Data = dtos, TotalCount = totalCount};
    }
    
    private static Expression<Func<T, object>> OrderByDynamic<T>(string fieldName)
    {
        fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);
        var entityType = typeof(T);
        var parameter = Expression.Parameter(entityType, "x");
        var property = entityType.GetProperty(fieldName);
        if (property == null)
        {
            throw new ArgumentException($"Property '{fieldName}' does not exist on type '{entityType.Name}'");
        }

        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var converted = Expression.Convert(propertyAccess, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, parameter);
    }
}