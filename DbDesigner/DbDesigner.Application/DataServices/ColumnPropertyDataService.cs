using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.ColumnProperty;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class ColumnPropertyDataService : BaseDataService<ColumnProperty, ColumnPropertyDto, FilterRequestDto, ComboboxDto>
{
    public ColumnPropertyDataService(
        IRepository<ColumnProperty> repository,
        IDataSourceHelper dataSourceHelper,
        IMapper mapper) : base(repository, dataSourceHelper, null, mapper)
    {
    }
}