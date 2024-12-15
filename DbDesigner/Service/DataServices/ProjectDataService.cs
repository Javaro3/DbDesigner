using AutoMapper;
using Common.Domain;
using Common.Dtos;
using Common.Dtos.ColumnProperty;
using Common.Dtos.Index;
using Common.Dtos.Project;
using Common.Dtos.Property;
using Common.Dtos.Relation;
using Common.Dtos.Table;
using Common.Enums;
using Common.Extensions;
using Common.GenerateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Repository.Repositories.Interfaces;
using Service.Interfaces.Infrastructure.DataServices;
using Service.Interfaces.Infrastructure.Infrastructure;
using Service.Interfaces.Infrastructure.Infrastructure.Builders;
using Service.Interfaces.Infrastructure.Infrastructure.Factories;
using Service.Interfaces.Infrastructure.Infrastructure.Helpers;

namespace Service.DataServices;

public class ProjectDataService : BaseDataService<Project, ProjectCardDto, ProjectFilterDto, ComboboxDto>, IProjectDataService
{
    private readonly IColumnPropertyRepository _columnPropertyRepository;
    private readonly IIndexRepository _indexRepository;
    private readonly IRelationRepository _relationRepository;
    private readonly ITableRepository _tableRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly IRepository<Property> _propertyRepository;
    private readonly IRepository<IndexColumn> _indexColumnRepository;
    private readonly IRepository<LanguageType> _languageTypeRepository;
    private readonly IFactory<IDataBaseBuilder> _dataBaseBuilderFactory;
    private readonly IFactory<IDomainBuilder> _domainBuilderFactory;
    private readonly IFactory<IOrmBuilder> _ormBuilderFactory;
    private readonly IFactory<IArchitectureBuilder?> _architectureBuilderFactory;
    private readonly ITcpManager _tcpManager;
    
    public ProjectDataService(
        IProjectRepository repository,
        IDataSourceHelper dataSourceHelper,
        IBaseHelper<Project, ProjectFilterDto> helper,
        IColumnPropertyRepository columnPropertyRepository,
        IIndexRepository indexRepository,
        IRelationRepository relationRepository,
        IMapper mapper,
        IFactory<IDataBaseBuilder> dataBaseBuilderFactory,
        ITableRepository tableRepository,
        IColumnRepository columnRepository,
        IRepository<Property> propertyRepository,
        IRepository<IndexColumn> indexColumnRepository,
        IRepository<LanguageType> languageTypeRepository,
        IFactory<IDomainBuilder> domainBuilderFactory,
        IFactory<IOrmBuilder> ormBuilderFactory,
        IFactory<IArchitectureBuilder> architectureBuilderFactory,
        ITcpManager tcpManager) : base(repository, dataSourceHelper, helper, mapper)
    {
        _columnPropertyRepository = columnPropertyRepository;
        _indexRepository = indexRepository;
        _relationRepository = relationRepository;
        _dataBaseBuilderFactory = dataBaseBuilderFactory;
        _tableRepository = tableRepository;
        _columnRepository = columnRepository;
        _propertyRepository = propertyRepository;
        _indexColumnRepository = indexColumnRepository;
        _languageTypeRepository = languageTypeRepository;
        _domainBuilderFactory = domainBuilderFactory;
        _ormBuilderFactory = ormBuilderFactory;
        _architectureBuilderFactory = architectureBuilderFactory;
        _tcpManager = tcpManager;
    }
    
    public override async Task UpdateAsync(ProjectCardDto dto)
    {
        var model = _mapper.Map<Project>(dto);
        if (model.Id == 0)
        {
            model.CreatedOn = DateTime.UtcNow;
            await _repository.AddAsync(model);
        }
        else
        {
            await _repository.UpdateAsync(model);
        }
    }

    public async Task<ProjectDiagramDto> GetForDiagramByIdAsync(int id)
    {
        var repository = _repository as IProjectRepository;
        var project = await repository?.GetForDiagramByIdAsync(id)!;
        var dto = _mapper.Map<ProjectDiagramDto>(project);
        
        foreach (var table in dto?.Tables!)
        {
            var tableColumnIds = table.Columns.Select(e => e.Id);
            var indexes = await _indexRepository.GetForDiagramAsync(tableColumnIds);
            table.Indexes = _mapper.Map<List<IndexDto>>(indexes);
            
            foreach (var column in table.Columns)
            {
                var columnProperties = await _columnPropertyRepository.GetForDiagramAsync(column.Id);
                column.Properties = _mapper.Map<List<ColumnPropertyDto>>(columnProperties);
            }
        }

        var relationColumnIds = dto.Tables.SelectMany(table => table.Columns).Select(column => column.Id);
        var relations = await _relationRepository.GetForDiagramAsync(relationColumnIds);
        dto.Relations = _mapper.Map<List<RelationDto>>(relations);
        
        return dto;
    }

    public async Task<ResultGenerateModel> GetDataBaseSqlScript(ProjectGenerateDto dto)
    {
        var dataBaseGenerateModel = await GetDataBaseGenerateModel(dto);
        var domainGenerateTask = GetDomainGenerateModels(dataBaseGenerateModel, dto);
        var databaseBuilderTask = Task.FromResult(_dataBaseBuilderFactory.GetEntity(dto.DataBaseId));
        var domainBuilderTask = Task.FromResult(_domainBuilderFactory.GetEntity(dto.LanguageId));
        var ormBuilderTask = Task.FromResult(_ormBuilderFactory.GetEntity(dto.OrmId));
        var architectureBuilderTask = Task.FromResult(_architectureBuilderFactory.GetEntity(dto.ArchitectureId));

        await Task.WhenAll(domainGenerateTask, databaseBuilderTask, domainBuilderTask, ormBuilderTask, architectureBuilderTask);

        var domainGenerateModels = domainGenerateTask.Result;
        var databaseBuilder = databaseBuilderTask.Result;
        var domainBuilder = domainBuilderTask.Result;
        var ormBuilder = ormBuilderTask.Result;
        var architectureBuilder = architectureBuilderTask.Result;

        var scriptTask = Task.Run(() => databaseBuilder
            .AddName(dataBaseGenerateModel.DataBaseName)
            .AddTables(dataBaseGenerateModel.Tables)
            .AddIndexes(dataBaseGenerateModel.Indexes)
            .Generate());

        var domainsTask = Task.Run(() =>
            domainGenerateModels
                .Select(e => (e.DomainName.ConvertToPascalCase(), domainBuilder
                    .AddDomainName(e.DomainName)
                    .AddFields(e.Fields)
                    .AddRelations(e.Relations)
                    .Generate()))
                .ToList());

        var ormGenerateModel = new OrmGenerateModel
        {
            OrmName = dataBaseGenerateModel.DataBaseName.ConvertToPascalCase() + "DbContext",
            Domains = domainGenerateModels.Select(e => e.DomainName.ConvertToPascalCase()).ToList()
        };

        var ormTask = Task.Run(() => ormBuilder
            .AddOrm(ormGenerateModel)
            .Generate());

        var architectureTask = architectureBuilder is not null
            ? Task.Run(() => architectureBuilder
                .AddName()
                .AddInterfaces()
                .AddImplementation(ormGenerateModel)
                .Generate())
            : Task.FromResult<ArchitectureGenerateModel?>(null);

        string? dataScript = null;
        if (dto.TableGenerateInfos.Any())
        {
            var dataModel = GetDataModel(dataBaseGenerateModel, dto.TableGenerateInfos.ToList());
            dataScript = await _tcpManager.SendGenerationDataAsync(dataModel);
        }

        await Task.WhenAll(scriptTask, domainsTask, ormTask, architectureTask);

        var result = new ResultGenerateModel
        {
            ProjectId = dto.ProjectId,
            CreateScript = scriptTask.Result,
            DataScript = dataScript,
            Domains = domainsTask.Result,
            Orm = (ormGenerateModel.OrmName, ormTask.Result),
            Architecture = architectureTask.Result
        };

        return result;
    }

    private async Task<DataBaseGenerateModel> GetDataBaseGenerateModel(ProjectGenerateDto dto)
    {
        var repository = _repository as IProjectRepository;
        var project = await repository.GetByIdAsync(dto.ProjectId);

        return new DataBaseGenerateModel
        {
            DataBaseName = project.Name.ConvertToPascalCase(),
            Tables = await GetTableGenerateModels(project.Id),
            Indexes = await GetIndexGenerateModels(project.Id)
        };
    }
    
    private async Task<IEnumerable<IndexGenerateModel>> GetIndexGenerateModels(int dataBaseId)
    {
        var models = new List<IndexGenerateModel>();
        var tableIds = await _tableRepository.GetAll()
            .Where(e => e.Projects.Select(p => p.Id).Contains(dataBaseId))
            .Select(e => e.Id)
            .ToListAsync();

        var columnIds = await _columnRepository.GetAll()
            .Where(e => tableIds.Any(x => e.Tables.Select(d => d.Id).Contains(x)))
            .Select(e => e.Id)
            .ToListAsync();

        var indexIds = await _indexColumnRepository.GetAll()
            .Where(e => columnIds.Contains(e.ColumnId))
            .Select(e => e.IndexId)
            .ToListAsync();

        var indexes = await _indexRepository.GetAll()
            .Include(e => e.Columns)
                .ThenInclude(e => e.Tables)
            .Where(e => indexIds.Contains(e.Id))
            .ToListAsync();

        foreach (var index in indexes)
        {
            models.Add(new IndexGenerateModel
            {
                IndexType = index.IndexTypeId,
                TableName = index.Columns.First().Tables.First().Name,
                ColumnNames = index.Columns.Select(e => e.Name)
            });
        }
        
        return models;
    }
    
    private async Task<IEnumerable<TableGenerateModel>> GetTableGenerateModels(int dataBaseId)
    {
        var models = new List<TableGenerateModel>();
        var tables = await _tableRepository.GetAll()
            .Where(e => e.Projects.Select(p => p.Id).Contains(dataBaseId))
            .ToListAsync();

        foreach (var table in tables)
        {
            models.Add(new TableGenerateModel
            {
                TableId = table.Id,
                TableName = table.Name,
                Columns = await GetColumnGenerateModels(table.Id),
                Relations = await GetRelationGenerateModels(table.Id)
            });
        }

        return models;
    }
    
    private async Task<IEnumerable<RelationGenerateModel>> GetRelationGenerateModels(int tableId)
    {
        var models = new List<RelationGenerateModel>();
        var columns = await _columnRepository.GetAll()
            .Where(e => e.Tables.Select(x => x.Id).Contains(tableId))
            .ToListAsync();
        
        var columnIds = columns.ConvertAll(e => e.Id);
        var relations = await _relationRepository.GetAll()
            .Include(e => e.OnDelete)
            .Include(e => e.OnUpdate)
            .Include(e => e.TargetColumn)
                .ThenInclude(e => e.Tables)
            .Include(e => e.SourceColumn)
                .ThenInclude(e => e.Tables)
            .Where(e => columnIds.Contains(e.TargetColumnId))
            .ToListAsync();

        foreach (var relation in relations)
        {
            models.Add(new RelationGenerateModel
            {
                OnDelete = relation.OnDelete.Id,
                OnUpdate = relation.OnUpdate.Id,
                SourceColumnId = relation.SourceColumnId,
                SourceColumnName = relation.SourceColumn.Name,
                TargetColumnId = relation.TargetColumnId,
                TargetColumnName = relation.TargetColumn.Name,
                SourceTableId = relation.SourceColumn.Tables.First().Id,
                SourceTableName = relation.SourceColumn.Tables.First().Name,
                TargetTableId = relation.TargetColumn.Tables.First().Id,
                TargetTableName = relation.TargetColumn.Tables.First().Name,
            });
        }

        return models;
    }
    
    private async Task<IEnumerable<ColumnGenerateModel>> GetColumnGenerateModels(int tableId)
    {
        var models = new List<ColumnGenerateModel>();
        var columns = await _columnRepository.GetAll()
            .Include(e => e.SqlType)
            .Where(e => e.Tables.Select(t => t.Id).Contains(tableId))
            .ToListAsync();
        
        foreach (var column in columns)
        {
            models.Add(new ColumnGenerateModel
            {
                ColumnId = column.Id,
                ColumnName = column.Name,
                SqlType = new SqlTypeGenerateModel
                {
                    SqlTypeId = column.SqlTypeId,
                    SqlTypeName = column.SqlType.Name,
                    Params = column.SqlTypeParams
                },
                Properties = await GetPropertyGenerateModels(column.Id)
            });
        }

        return models;
    }
    
    private async Task<IEnumerable<PropertyGenerateModel>> GetPropertyGenerateModels(int columnId)
    {
        var models = new List<PropertyGenerateModel>();
        var properties = await _columnPropertyRepository.GetAll()
            .Where(e => e.ColumnId == columnId)
            .ToListAsync();
        
        foreach (var property in properties)
        {
            models.Add(new PropertyGenerateModel
            {
                Property = (await _propertyRepository.GetByIdAsync(property.PropertyId)).Id,
                Params = property.PropertyParams
            });
        }

        return models;
    }
    
    private  DataProjectGenerateModel GetDataModel(DataBaseGenerateModel dataBaseModel, List<TableGenerateDto> tables)
    {
        var models = new DataProjectGenerateModel
        {
            DatabaseName = dataBaseModel.DataBaseName,
            Tables = tables.Select(e =>
            {
                var table = dataBaseModel.Tables.First(x => x.TableId == e.TableId);
                return new DataTableGenerateModel
                {
                    TableId = e.TableId,
                    TableName = table.TableName,
                    RowCount = e.RowCount,
                    Fields = table.Columns
                        .Where(x => !x.Properties.Select(y => y.Property).Contains((int)PropertyEnum.Increment))
                        .Select(x => new DataFieldGenerateModel
                        {
                            FieldName = x.ColumnName,
                            FieldType = x.SqlType.SqlTypeName,
                            RelationTableId = table.Relations
                                .FirstOrDefault(r => r.TargetColumnId == x.ColumnId)
                                ?.SourceTableId
                        }).ToList()
                };
            }).ToList()
        };

        return models;
    }
    
    private async Task<IEnumerable<DomainGenerateModel>> GetDomainGenerateModels(DataBaseGenerateModel database, ProjectGenerateDto dto)
    {
        var models = new List<DomainGenerateModel>();
        var relations = new List<RelationGenerateModel>();
        var fieldTypes = await _languageTypeRepository.GetAll().ToListAsync();
        
        foreach (var table in database.Tables)
        {
            var columnIds = table.Columns.Select(e => e.ColumnId).ToList();
            var relationIds = table.Relations.Select(e => e.TargetColumnId).ToList();
            
            relations.AddRange(table.Relations);
            if (columnIds.OrderBy(x => x).SequenceEqual(relationIds.OrderBy(x => x)))
                continue;
            
            var model = new DomainGenerateModel
            {
                DomainId = table.TableId,
                DomainName = table.TableName
            };
            var fields = new List<FieldGenerateModel>();
            foreach (var column in table.Columns)
            {
                var fieldType = fieldTypes.FirstOrDefault(e => e.LanguageId == dto.LanguageId && e.SqlTypes.Select(x => x.Id).Contains(column.SqlType.SqlTypeId));

                fields.Add(new FieldGenerateModel
                {
                    FieldId = column.ColumnId,
                    FieldType = fieldType.Name,
                    FieldName = column.ColumnName
                });
                
            }

            model.Fields = fields;
            models.Add(model);
        }

        var usedRelation = new List<RelationGenerateModel>();
        foreach (var relation in relations)
        {
            if (usedRelation.Contains(relation))
                continue;
            
            if (IsManyToManyForRelation(relations, relation))
            {
                var secondRelation = relations.First(e => e.TargetTableId == relation.TargetTableId && e.SourceTableId != relation.SourceTableId);
                
                var sourceRelation = new FieldRelationGenerationModel
                {
                    RelationDomainName = secondRelation.SourceTableName,
                    RelationType = RelationTypeEnum.ManyToMany
                };
            
                var targetRelation = new FieldRelationGenerationModel
                {
                    RelationDomainName = relation.SourceTableName,
                    RelationType = RelationTypeEnum.ManyToMany
                };
                
                var sourceTable = models.FirstOrDefault(e => e.DomainId == relation.SourceTableId);
                sourceTable?.Relations.Add(sourceRelation);
                
                var targetTable = models.FirstOrDefault(e => e.DomainId == secondRelation.SourceTableId);
                targetTable?.Relations.Add(targetRelation);
                
                usedRelation.Add(secondRelation);
            }
            else
            {
                var sourceRelation = new FieldRelationGenerationModel
                {
                    RelationDomainName = relation.TargetTableName,
                    RelationType = RelationTypeEnum.ManyToMany
                };
            
                var targetRelation = new FieldRelationGenerationModel
                {
                    RelationDomainName = relation.SourceTableName,
                    RelationType = RelationTypeEnum.ManyToOne
                };

                var sourceTable = models.FirstOrDefault(e => e.DomainId == relation.SourceTableId);
                sourceTable?.Relations.Add(sourceRelation);
            
                var targetTable = models.FirstOrDefault(e => e.DomainId == relation.TargetTableId);
                targetTable?.Relations.Add(targetRelation);
            }
        }
        
        return models;
    }
    
    public static bool IsManyToManyForRelation(List<RelationGenerateModel> allRelations, RelationGenerateModel relation)
    {
        var potentialRelations = allRelations
            .Where(r => r.TargetTableId == relation.TargetTableId)
            .ToList();

        var uniqueSourceTables = potentialRelations
            .Select(r => r.SourceTableId)
            .Distinct();

        return uniqueSourceTables.Count() > 1;
    }
}