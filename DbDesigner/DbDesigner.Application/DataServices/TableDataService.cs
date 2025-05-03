using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Table;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class TableDataService : BaseDataService<Table, TableDto, FilterRequestDto, ComboboxDto>
{
    public TableDataService(
        IRepository<Table> repository,
        IDataSourceHelper dataSourceHelper,
        IMapper mapper) : base(repository, dataSourceHelper, null, mapper)
    {
    }
}