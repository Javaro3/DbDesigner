using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.SqlType;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class SqlTypeDataService : BaseDataService<SqlType, SqlTypeDto, SqlTypeFilterDto, HasParamsComboboxDto>, ISqlTypeDataService
{
    public SqlTypeDataService(
        IRepository<SqlType> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<SqlType, SqlTypeFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }

    public List<HasParamsComboboxDto> GetForComboboxByDataBase(int dataBaseId)
    {
        var query = _repository.GetAll().Where(e => e.DataBases.Select(d => d.Id).Contains(dataBaseId));
        var dtos = _mapper.Map<List<HasParamsComboboxDto>>(query);
        return dtos;
    }
}