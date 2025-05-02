using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators.DalGenerators;

public abstract class BaseDalGenerator : IDalGenerator
{
    protected readonly IProjectStorageManager ProjectStorageManager;
    protected readonly ProjectStorageOptions ProjectStorageOptions;
    protected readonly DatabaseConfigOptions? Options;
    protected readonly TemplateStorageOptions? TemplateStorageOptions;

    public BaseDalGenerator(
        IProjectStorageManager projectStorageManager,
        DatabaseConfigOptions? options,
        TemplateStorageOptions? templateStorageOptions,
        ProjectStorageOptions projectStorageOptions)
    {
        ProjectStorageManager = projectStorageManager;
        Options = options;
        TemplateStorageOptions = templateStorageOptions;
        ProjectStorageOptions = projectStorageOptions;
    }
    
    public abstract Task<string?> GenerateAsync(DalGeneratorRequestDto model);
}