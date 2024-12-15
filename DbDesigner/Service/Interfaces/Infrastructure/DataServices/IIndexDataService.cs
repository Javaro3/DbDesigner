using Common.Dtos.Index;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IIndexDataService
{
    Task<IndexDto> AddAsync(IndexDto index);
    
    Task UpdateAsync(IndexDto dto);
    
    Task DeleteAsync(int id);
}