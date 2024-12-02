using AutoMapper;
using Common.Domain;
using Common.Dtos.Column;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;

namespace Service.DataServices;

public class ColumnDataService : IColumnDataService
{
    private readonly IColumnRepository _repository;
    private readonly IMapper _mapper;

    public ColumnDataService(IColumnRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ColumnDiagramDto> AddColumnToTableAsync(ColumnBaseDto table, int projectId)
    {
        var model = _mapper.Map<Column>(table);
        await _repository.AddColumnToTableAsync(model, projectId);

        var newTableDto = _mapper.Map<ColumnDiagramDto>(model);
        return newTableDto;
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model != null)
            await _repository.DeleteAsync(model);
    }

    public async Task UpdateAsync(ColumnBaseDto table)
    {
        var model = _mapper.Map<Column>(table);
        await _repository.UpdateAsync(model);
    }
}