using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Architecture;
using DbDesigner.Application.Dtos.Index;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Application.DataServices;

public class IndexDataService : BaseDataService<Index, IndexDto, FilterRequestDto, ComboboxDto>
{
    public IndexDataService(
        IRepository<Index> repository,
        IDataSourceHelper dataSourceHelper, 
        IMapper mapper) : base(repository, dataSourceHelper, null, mapper)
    {
    }
}