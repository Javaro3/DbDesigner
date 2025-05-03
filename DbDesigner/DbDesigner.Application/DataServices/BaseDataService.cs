using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain.BaseDomain;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DbDesigner.Application.DataServices;

public class BaseDataService<TModel, TDto, TFilterDto, TComboboxDto> : IBaseDataService<TModel, TDto, TFilterDto, TComboboxDto> 
    where TModel : BaseModel, IHasId
    where TFilterDto : FilterRequestDto
{
    protected readonly IRepository<TModel> Repository;
    protected readonly IDataSourceHelper DataSourceHelper;
    protected readonly IBaseHelper<TModel, TFilterDto>? Helper;
    protected readonly IMapper Mapper;
    
    public BaseDataService(
        IRepository<TModel> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<TModel, TFilterDto>? helper,
        IMapper mapper)
    {
        Repository = repository;
        DataSourceHelper = dataSourceHelper;
        Helper = helper;
        Mapper = mapper;
    }

    public virtual async Task<TransportDto<TDto>> GetFilteredAsync(TFilterDto filter)
    {
        var query = Repository.Get();
        if (Helper is not null)
        {
            query = Helper.ApplyFilter(query, filter);
            query = Helper.ApplySort(query, filter);
        }

        var transportDto = await DataSourceHelper.ApplyDataSource<TModel, TDto>(query, filter, Mapper);
        return transportDto;
    }

    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        var model = await Repository.GetAsync(id);
        var dto = Mapper.Map<TDto>(model);
        return dto;
    }

    public virtual List<TComboboxDto> GetForCombobox()
    {
        var query = Repository.Get();
        var dtos = Mapper.Map<List<TComboboxDto>>(query);
        return dtos;
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        var model = Mapper.Map<TModel>(dto);
        return Mapper.Map<TDto>(model.Id == 0
            ? await Repository.AddAsync(model)
            : await Repository.UpdateAsync(model));
    }

    public virtual async Task DeleteAsync(int id)
    {
        var model = await Repository.GetAsync(id);
        if (model != null)
            await Repository.DeleteAsync(model);
    }
}