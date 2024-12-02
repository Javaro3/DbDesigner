using AutoMapper;
using Common.Dtos;

namespace Service.Interfaces.Infrastructure.Infrastructure.Helpers;

public interface IDataSourceHelper
{
    Task<TransportDto<TDto>> ApplyDataSource<T, TDto>(IQueryable<T> query, FilterRequestDto filter, IMapper mapper);
}