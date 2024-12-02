using AutoMapper;
using Common.Domain;
using Common.Dtos.Table;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;

namespace Service.DataServices;

public class TableDataService : ITableDataService
{
    private readonly ITableRepository _repository;
    private readonly IMapper _mapper;

    public TableDataService(ITableRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TableDiagramDto> AddTableToProjectAsync(TableBaseDto table, int projectId)
    {
        var model = _mapper.Map<Table>(table);
        await _repository.AddTableToProjectAsync(model, projectId);

        var newTableDto = _mapper.Map<TableDiagramDto>(model);
        return newTableDto;
    }

    public async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model != null)
            await _repository.DeleteAsync(model);
    }

    public async Task UpdateAsync(TableBaseDto table)
    {
        var model = _mapper.Map<Table>(table);
        await _repository.UpdateAsync(model);
    }
}