using Common.Domain.BaseDomain;
using Common.Dtos;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IBaseDataService<TModel, TDto, TFilterDto, TComboboxDto> 
    where TModel : BaseModel 
    where TFilterDto : FilterRequestDto
{
    Task<TransportDto<TDto>> GetFilteredAsync(TFilterDto filter);
    
    Task<TDto> GetByIdAsync(int id);
    
    List<TComboboxDto> GetForCombobox();
    
    Task UpdateAsync(TDto dto);
    
    Task DeleteAsync(int id);
}