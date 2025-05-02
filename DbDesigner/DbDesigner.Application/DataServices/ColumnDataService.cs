using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Column;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class ColumnDataService : BaseDataService<Column, ColumnDto, FilterRequestDto, ComboboxDto>
{
    public ColumnDataService(
        IRepository<Column> repository,
        IDataSourceHelper dataSourceHelper,
        IMapper mapper) : base(repository, dataSourceHelper, null, mapper)
    {
    }
}