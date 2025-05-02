using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class DataBaseDataService : BaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto>
{
    public DataBaseDataService(
        IRepository<DataBase> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<DataBase, DataBaseFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}