using Common.Domain;
using Common.Dtos;
using Common.Dtos.DataBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "Administrator")]
public class DataBaseController : Controller
{
    private readonly IBaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto> _dataBaseDataService;

    public DataBaseController(IBaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto> dataBaseDataService)
    {
        _dataBaseDataService = dataBaseDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] DataBaseFilterDto filter)
    {
        var data = await _dataBaseDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _dataBaseDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _dataBaseDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] DataBaseDto dto)
    {
        try
        {
            await _dataBaseDataService.UpdateAsync(dto);
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
            await _dataBaseDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}