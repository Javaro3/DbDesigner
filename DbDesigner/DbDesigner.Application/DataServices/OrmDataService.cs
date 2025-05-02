using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Orm;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

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
        var query = Repository.Get().Where(e => e.LanguageId == languageId);
        var dtos = Mapper.Map<List<ComboboxDto>>(query);
        return dtos;
    }
}