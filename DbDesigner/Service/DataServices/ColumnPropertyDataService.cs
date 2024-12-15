using AutoMapper;
using Common.Domain;
using Common.Dtos.ColumnProperty;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;

namespace Service.DataServices;

public class ColumnPropertyDataService : IColumnPropertyDataService
{
    private readonly IColumnPropertyRepository _repository;
    private readonly IMapper _mapper;

    public ColumnPropertyDataService(IColumnPropertyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AddPropertyToColumnAsync(ColumnPropertyDto columnProperty, int columnId)
    {
        var model = _mapper.Map<ColumnProperty>(columnProperty);
        await _repository.AddPropertyToColumnAsync(model, columnId);
    }

    public async Task DeleteAsync(ColumnPropertyDeleteDto columnProperty)
    {
        var model = _mapper.Map<ColumnProperty>(columnProperty);
        await _repository.DeleteAsync(model);
    }

    public async Task UpdateAsync(ColumnPropertyUpdateDto columnProperty)
    {
        var model = _mapper.Map<ColumnProperty>(columnProperty);
        await _repository.UpdatePropertyToColumnAsync(model, columnProperty.PrevPropertyId);
    }
}