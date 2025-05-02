using AutoMapper;
using DbDesigner.Application.Dtos;

namespace DbDesigner.Application.Interfaces.Helpers;

public interface IDataSourceHelper
{
    Task<TransportDto<TDto>> ApplyDataSource<T, TDto>(IQueryable<T> query, FilterRequestDto filter, IMapper mapper);
}