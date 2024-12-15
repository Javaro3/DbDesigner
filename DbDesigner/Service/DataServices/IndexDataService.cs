using AutoMapper;
using Common.Dtos.Index;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Index = Common.Domain.Index;

namespace Service.DataServices;

public class IndexDataService : IIndexDataService
{
    private readonly IIndexRepository _repository;
    private readonly IMapper _mapper;

    public IndexDataService(IIndexRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IndexDto> AddAsync(IndexDto index)
    {
        var model = _mapper.Map<Index>(index);
        await _repository.AddAsync(model);
        
        var newIndexDto = _mapper.Map<IndexDto>(model);
        return newIndexDto;
    }

    public virtual async Task UpdateAsync(IndexDto dto)
    {
        var model = _mapper.Map<Index>(dto);
        await _repository.UpdateAsync(model);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model != null)
            await _repository.DeleteAsync(model);
    }
}