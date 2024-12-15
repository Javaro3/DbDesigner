using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.Orm;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class OrmDataService : BaseDataService<Orm, OrmDto, OrmFilterDto, ComboboxDto>, IOrmDataService
{
    public OrmDataService(
        IRepository<Orm> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Orm, OrmFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }

    public List<ComboboxDto> GetForComboboxByLanguage(int languageId)
    {
        var query = _repository.GetAll().Where(e => e.Languages.Any(x => x.Id == languageId));
        var dtos = _mapper.Map<List<ComboboxDto>>(query);
        return dtos;
    }
}