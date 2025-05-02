using DbDesigner.Application.Dtos;

namespace DbDesigner.Application.Interfaces.DataServices;

public interface IBaseDataService<TModel, TDto, TFilterDto, TComboboxDto> where TFilterDto : FilterRequestDto
{
    Task<TransportDto<TDto>> GetFilteredAsync(TFilterDto filter);
    
    Task<TDto> GetByIdAsync(int id);
    
    List<TComboboxDto> GetForCombobox();
    
    Task<TDto> UpdateAsync(TDto dto);
    
    Task DeleteAsync(int id);
}