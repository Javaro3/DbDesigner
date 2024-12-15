using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.DataBase;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

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