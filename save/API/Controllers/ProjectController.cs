using Common.Dtos.Project;
using Common.Dtos.Property;
using Common.Dtos.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Infrastructure.DataServices;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "User")]
public class ProjectController : Controller
{
    private readonly IProjectDataService _projectDataService;
    private readonly IProjectStorageDataService _projectStorageDataService;
    private readonly IUserDataService _userDataService;


    public ProjectController(
        IProjectDataService projectDataService,
        IUserDataService userDataService,
        IProjectStorageDataService projectStorageDataService)
    {
        _projectDataService = projectDataService;
        _userDataService = userDataService;
        _projectStorageDataService = projectStorageDataService;
    }
    
    [HttpGet("get-all")]
    public async Task<IResult> GetAll([FromQuery] ProjectFilterDto filter)
    {
        var currentUserId = (await _userDataService.GetCurrentUserAsync(Request.Headers.Authorization!))!.Id;
        filter.CurrentUser = currentUserId;
            
        var data = await _projectDataService.GetFilteredAsync(filter);
        return Results.Json(data);
    }
    
    [HttpGet("get")]
    public async Task<IResult> Get(int id)
    {
        var data = await _projectDataService.GetByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("get-combobox")]
    public IResult GetForCombobox()
    {
        var data = _projectDataService.GetForCombobox();
        return Results.Json(data);
    }
    
    [HttpPost("update")]
    public async Task<IResult> Update([FromBody] ProjectCardDto dto)
    {
        try
        {
            var currentUserId = (await _userDataService.GetCurrentUserAsync(Request.Headers.Authorization!))!.Id;
            if (dto.Users.All(e => e.Id != currentUserId))
            {
                dto.Users.Add(new UserDto {Id = currentUserId});
            }
            await _projectDataService.UpdateAsync(dto);
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
            await _projectDataService.DeleteAsync(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    [HttpGet("get-for-diagram")]
    public async Task<IResult> GetForDiagram([FromQuery] int id)
    {
        var data = await _projectDataService.GetForDiagramByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpPost("generate")]
    public async Task<IResult> Generate([FromBody] ProjectGenerateDto generateDto)
    {
        var result = await _projectDataService.GetDataBaseSqlScript(generateDto);
        await _projectStorageDataService.SaveGenerationResultModel(result);

        var fileBytes = await _projectStorageDataService.GetProject(generateDto.ProjectId);
        return Results.File(fileBytes, "", "result.zip");
    }
    
    [HttpGet("download")]
    public async Task<IResult> Download([FromQuery] int projectId)
    {
        var fileBytes = await _projectStorageDataService.GetProject(projectId);
        return Results.File(fileBytes, "", "result.zip");
    }
}