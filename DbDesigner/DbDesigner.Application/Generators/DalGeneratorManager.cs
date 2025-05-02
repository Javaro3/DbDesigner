using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Generators.DalGenerators;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Microsoft.Extensions.Options;

namespace DbDesigner.Application.Generators;

public class DalGeneratorManager : IDalGeneratorManager
{
    private readonly IProjectStorageManager _projectStorageManager;
    private readonly ProjectStorageOptions _projectStorageOptions;
    private readonly DatabaseOptions _options;
    private readonly TemplateStorageOptions _templateStorageOptions;

    public DalGeneratorManager(
        IProjectStorageManager projectStorageManager,
        IOptions<DatabaseOptions> options,
        IOptions<TemplateStorageOptions> templateStorageOptions,
        IOptions<ProjectStorageOptions> projectStorageOptions)
    {
        _projectStorageManager = projectStorageManager;
        _projectStorageOptions = projectStorageOptions.Value;
        _templateStorageOptions = templateStorageOptions.Value;
        _options = options.Value;
    } 
    
    public async Task<string?> GenerateAsync(DalGeneratorRequestDto model)
    {
        try
        {
            var dataBaseOptions = GetDataBaseConfigOptions((DataBaseEnum)model.DataBaseId);
            var dalGenerator = GetDalGenerator((LanguageEnum?)model.LanguageId, dataBaseOptions);

            return dalGenerator is null
                ? "This database is not supported"
                : await dalGenerator.GenerateAsync(model);
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    
    private DatabaseConfigOptions? GetDataBaseConfigOptions(DataBaseEnum? dataBase)
    {
        return dataBase switch
        {
            DataBaseEnum.MySql => _options.MySql,
            DataBaseEnum.PostgreSql => _options.PostgreSql,
            DataBaseEnum.SqLite => _options.SqLite,
            DataBaseEnum.MsSql => _options.MsSql,
            _ => throw new ArgumentException("Database is not supported")
        };
    }
    
    private IDalGenerator? GetDalGenerator(LanguageEnum? language, DatabaseConfigOptions? databaseOptions)
    {
        return language switch
        {
            LanguageEnum.CSharp => new CSharpGenerator(
                _projectStorageManager,
                databaseOptions,
                _templateStorageOptions,
                _projectStorageOptions),
            LanguageEnum.JavaScript => new JavaScriptGenerator(
                _projectStorageManager,
                databaseOptions,
                _templateStorageOptions,
                _projectStorageOptions),
            LanguageEnum.Python => new PythonGenerator(
                _projectStorageManager,
                databaseOptions,
                _templateStorageOptions,
                _projectStorageOptions),
            _ => null
        };
    }
}