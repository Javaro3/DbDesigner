using AutoMapper;
using Common.Domain;
using Common.Dtos.Relation;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;

namespace Service.DataServices;

public class RelationDataService : IRelationDataService
{
    private readonly IRelationRepository _repository;
    private readonly IMapper _mapper;


    public RelationDataService(IRelationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RelationDto> AddToProjectAsync(RelationDto relation)
    {
        var model = _mapper.Map<Relation>(relation);
        await _repository.AddAsync(model);

        relation = _mapper.Map<RelationDto>(model);
        return relation;
    }

    public async Task UpdateAsync(RelationDto relation)
    {
        var model = _mapper.Map<Relation>(relation);
        await _repository.UpdateAsync(model);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var model = await _repository.GetByIdAsync(id);
        if (model != null)
            await _repository.DeleteAsync(model);
    }
}