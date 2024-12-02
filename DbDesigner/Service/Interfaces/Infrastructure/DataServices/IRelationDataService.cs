using Common.Dtos.Relation;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IRelationDataService
{
    Task<RelationDto> AddToProjectAsync(RelationDto relation);
    
    Task UpdateAsync(RelationDto relation);
    
    Task DeleteAsync(int id);
}