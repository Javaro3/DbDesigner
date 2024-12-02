using Common.Domain;
using Common.Dtos;
using Common.Dtos.LanguageType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class LanguageTypeController : Controller
{
    private readonly IBaseDataService<LanguageType, LanguageTypeDto, LanguageTypeFilterDto, ComboboxDto> _languageTypeDataService;

    public LanguageTypeController(IBaseDataService<LanguageType, LanguageTypeDto, LanguageTypeFilterDto, ComboboxDto> languageTypeDataService)
    {
        _languageTypeDataService = languageTypeDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] LanguageTypeFilterDto filter)
    {
        var data = await _languageTypeDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _languageTypeDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _languageTypeDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] LanguageTypeDto dto)
    {
        try
        {
            await _languageTypeDataService.UpdateAsync(dto);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IResult> Delete([FromQuery] int id)
    {
        try
        {
            await _languageTypeDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}