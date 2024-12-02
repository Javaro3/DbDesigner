using AutoMapper;
using Common.Domain.BaseDomain;
using Common.Dtos;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class BaseDataService<TModel, TDto, TFilterDto, TComboboxDto> : IBaseDataService<TModel, TDto, TFilterDto, TComboboxDto> 
    where TModel : BaseModel, IHasId
    where TFilterDto : FilterRequestDto
{
    protected readonly IRepository<TModel> _repository;
    protected readonly IDataSourceHelper _dataSourceHelper;
    protected readonly IBaseHelper<TModel, TFilterDto> _helper;
    protected readonly IMapper _mapper;
    
    public BaseDataService(
        IRepository<TModel> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<TModel, TFilterDto> helper,
        IMapper mapper)
    {
        _repository = repository;
        _dataSourceHelper = dataSourceHelper;
        _helper = helper;
        _mapper = mapper;
    }

    public virtual async Task<TransportDto<TDto>> GetFilteredAsync(TFilterDto filter)
    {
        var query = _repository.GetAll();
        query = _helper.ApplyFilter(query, filter);
        query = _helper.ApplySort(query, filter);
        
        var transportDto = await _dataSourceHelper.ApplyDataSource<TModel, TDto>(query, filter, _mapper);
        return transportDto;
    }

    public virtual async Task<TDto> GetByIdAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        var dto = _mapper.Map<TDto>(model);
        return dto;
    }

    public virtual List<TComboboxDto> GetForCombobox()
    {
        var query = _repository.GetAll();
        var dtos = _mapper.Map<List<TComboboxDto>>(query);
        return dtos;
    }

    public virtual async Task UpdateAsync(TDto dto)
    {
        var model = _mapper.Map<TModel>(dto);
        if (model.Id == 0)
        {
            await _repository.AddAsync(model);
        }
        else
        {
            await _repository.UpdateAsync(model);
        }
    }

    public virtual async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model != null)
            await _repository.DeleteAsync(model);
    }
}