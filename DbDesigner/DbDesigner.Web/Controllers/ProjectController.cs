using System.Text;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Dtos.Project;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class ProjectController(IProjectDataService dataService)
    : BaseController<Project, ProjectDto, ProjectFilterDto, ComboboxDto>(dataService)
{
    [HttpGet("get-for-diagram")]
    public async Task<IResult> GetForDiagram([FromQuery] int id)
    {
        var data = await dataService.GetForDiagramByIdAsync(id);
        return Results.Json(data);
    }
    
    [HttpGet("generate-script/{projectId:int}")]
    public async Task<IResult> GenerateScript(int projectId)
    {
        var dto = await dataService.GenerateScriptAsync(projectId);
        return Results.Json(dto);
    }
    
    [HttpPost("generate-dal")]
    public async Task<IResult> GenerateDal(DalGeneratorRequestDto model)
    {
        var dto = await dataService.GenerateDalAsync(model);
        return Results.Json(dto);
    }

    [HttpPost("script-is-valid")]
    public async Task<IResult> ScriptIdValid([FromBody] SqlScriptRequestDto model)
    {
        var errors = await dataService.ScriptIsValidAsync(model);
        return Results.Json(new {errors});
    }
    
    [HttpGet("download-script/{projectId:int}")]
    public async Task<IResult> DownloadScript(int projectId)
    {
        var script = await dataService.GetScriptAsync(projectId);

        if (string.IsNullOrEmpty(script))
        {
            return Results.NotFound("Script not found.");
        }

        return Results.File(
            Encoding.UTF8.GetBytes(script),
            "application/sql",
            "script.sql");
    }
    
    [HttpGet("download/{projectId:int}")]
    public async Task<IResult> Download(int projectId)
    {
        var fileBytes = await dataService.GetProjectFilesAsync(projectId);
        return Results.File(fileBytes, "application/zip", "result.zip");
    }
}