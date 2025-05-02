using DbDesigner.Application.Dtos;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain.BaseDomain;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class BaseController<TModel, TDto, TFilterDto, TCombobox>(
    IBaseDataService<TModel, TDto, TFilterDto, TCombobox> dataService)
    : Controller
    where TModel : BaseModel
    where TFilterDto : FilterRequestDto
{
    protected readonly IBaseDataService<TModel, TDto, TFilterDto, TCombobox> DataService = dataService;

    [HttpGet]
    public virtual async Task<IResult> GetAll([FromQuery] TFilterDto filter)
    {
        var data = await DataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("{id:int}")]
    public virtual async Task<IResult> Get(int id)
    {
        var data = await DataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("combobox")]
    public virtual IResult GetForCombobox()
    {
        var data = DataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost]
    public virtual async Task<IResult> Update([FromBody] TDto dto)
    {
        try
        {
            var result = await DataService.UpdateAsync(dto);
            return Results.Json(result);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public virtual async Task<IResult> Delete(int id)
    {
        try
        {
            await DataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}