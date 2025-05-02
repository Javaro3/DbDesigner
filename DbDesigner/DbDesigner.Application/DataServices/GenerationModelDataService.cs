using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationModel;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Infrastructure.Repositories.Interfaces;

namespace DbDesigner.Application.DataServices;

public class GenerationModelDataService : BaseDataService<GenerationModel, GenerationModelDto, GenerationModelFilterDto, ComboboxDto>
{
    public GenerationModelDataService(
        IRepository<GenerationModel> repository,
        IDataSourceHelper dataSourceHelper, 
        IBaseHelper<GenerationModel, GenerationModelFilterDto> helper,
        IMapper mapper) : base(repository, dataSourceHelper, helper, mapper)
    {
    }
}