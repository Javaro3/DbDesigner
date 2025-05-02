using AutoMapper;
using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Dtos.Index;
using DbDesigner.Application.Dtos.Project;
using DbDesigner.Application.Dtos.Relation;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Application.Interfaces.Helpers;
using DbDesigner.Domain.Domain;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using DbDesigner.Infrastructure.Repositories.Interfaces;
using Index = DbDesigner.Domain.Domain.Index;

namespace DbDesigner.Application.DataServices;

public class ProjectDataService : BaseDataService<Project, ProjectDto, ProjectFilterDto, ComboboxDto>, IProjectDataService
{
    private readonly IRepository<Index> _indexRepository;
    private readonly IRepository<Relation> _relationRepository;
    private readonly ISqlScriptGenerator _sqlScriptGenerator;
    private readonly ISqlScriptValidator _sqlScriptValidator;
    private readonly IProjectStorageManager _projectStorageManager;
    private readonly IDalGeneratorManager _dalGeneratorManager;
    private readonly IDataGenerator _dataGenerator;
    
    public ProjectDataService(
        IRepository<Project> repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Project, ProjectFilterDto> helper,
        IRepository<Index> indexRepository,
        IRepository<Relation> relationRepository,
        IMapper mapper,
        ISqlScriptGenerator sqlScriptGenerator,
        ISqlScriptValidator sqlScriptValidator,
        IProjectStorageManager projectStorageManager,
        IDalGeneratorManager dalGeneratorManager,
        IDataGenerator dataGenerator) : base(repository, dataSourceHelper, helper, mapper)
    {
        _indexRepository = indexRepository;
        _relationRepository = relationRepository;
        _sqlScriptGenerator = sqlScriptGenerator;
        _sqlScriptValidator = sqlScriptValidator;
        _projectStorageManager = projectStorageManager;
        _dalGeneratorManager = dalGeneratorManager;
        _dataGenerator = dataGenerator;
    }
    
    public override async Task<ProjectDto> UpdateAsync(ProjectDto dto)
    {
        var model = Mapper.Map<Project>(dto);
        model.CreatedOn = model.Id == 0
            ? DateTime.UtcNow
            : model.CreatedOn;

        var result = model.Id == 0
            ? await Repository.AddAsync(model)
            : await Repository.UpdateAsync(model);

        return Mapper.Map<ProjectDto>(result);
    }

    public async Task<ProjectDiagramDto> GetForDiagramByIdAsync(int id)
    {
        var project = await Repository.GetAsync(id);
        var dto = Mapper.Map<ProjectDiagramDto>(project);
        
        foreach (var table in dto.Tables)
        {
            var tableColumnIds = table.Columns.Select(e => e.Id);
            var indexes = await _indexRepository.Get()
                .Where(e => e.Columns.Any(i => tableColumnIds.Contains(i.Id)))
                .ToListAsync();
            table.Indexes = Mapper.Map<List<IndexDto>>(indexes);
        }

        var relationColumnIds = dto.Tables
            .SelectMany(table => table.Columns)
            .Select(column => column.Id)
            .ToList();
        
        var relations = await _relationRepository.Get()
            .Where(e => relationColumnIds.Contains(e.SourceColumnId) || relationColumnIds.Contains(e.TargetColumnId))
            .ToListAsync();
        
        dto.Relations = Mapper.Map<List<RelationDto>>(relations);
        return dto;
    }

    public async Task<SqlScriptGenerateResultDto> GenerateScriptAsync(int projectId)
    {
        var projectModel = await GetForDiagramByIdAsync(projectId);
        var script = await _sqlScriptGenerator.GenerateScriptAsync(projectModel);
        var errors = await _sqlScriptValidator.ValidateScriptAsync(script, projectId.ToString(), (DataBaseEnum?)projectModel.DataBase?.Id);
        
        if (errors is null)
        {
            await _projectStorageManager.SaveScriptAsync(script, projectId);
        }
        
        return new SqlScriptGenerateResultDto { Script = script, Errors = errors };
    }

    public async Task<string?> ScriptIsValidAsync(SqlScriptRequestDto model)
    {
        var dataBaseId = (await Repository.GetAsync(model.ProjectId))?.DataBaseId;
        var errors = await _sqlScriptValidator.ValidateScriptAsync(model.Script, model.ProjectId.ToString(), (DataBaseEnum?)dataBaseId);

        if (errors is null)
        {
            await _projectStorageManager.SaveScriptAsync(model.Script, model.ProjectId);
        }
        
        return errors;
    }
    
    public async Task<string?> GetScriptAsync(int projectId)
    {
        return await _projectStorageManager.GetScriptAsync(projectId);
    }

    public async Task<byte[]> GetProjectFilesAsync(int projectId)
    {
        return await _projectStorageManager.GetProjectFilesAsync(projectId);
    }

    public async Task<DalGenerateResultDto> GenerateDalAsync(DalGeneratorRequestDto model)
    {
        var result = new DalGenerateResultDto();

        if (model.GenerateDal)
        {
            result.Errors = await _dalGeneratorManager.GenerateAsync(model);
        }

        if (result.Errors is null && model.GenerateTestData)
        {
            var project = await GetForDiagramByIdAsync(model.ProjectId);

            var dataGeneratorRequest = new DataGeneratorRequestDto
            {
                Project = project,
                GenerationLanguage = ((GenerationLanguageEnum)model.GenerationLanguageId).GetName(),
                GenerationModelId = model.GenerationModelId,
                TableGenerateInfos = model.TableGenerateInfos
            };

            result.Errors = await _dataGenerator.GenerateAsync(dataGeneratorRequest);
        }

        return result;
    }
}