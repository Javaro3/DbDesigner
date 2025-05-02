using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators.DalGenerators;

public class JavaScriptGenerator : BaseDalGenerator, IDalGenerator
{
    private readonly Dictionary<int, string> _packages = new()
    {
        {(int)DataBaseEnum.MySql, "mysql2 @types/mysql"},
        {(int)DataBaseEnum.PostgreSql, "pg @types/pg"},
        {(int)DataBaseEnum.SqLite, "sqlite3 @types/sqlite3"},
        {(int)DataBaseEnum.MsSql, "tedious @types/tedious"}
    };
    private readonly Dictionary<int, string> _architectures = new()
    {
        {(int)ArchitectureEnum.DirectDataAccess, "DDA"},
        {(int)ArchitectureEnum.RepositoryPattern, "Repository"},
        {(int)ArchitectureEnum.CommandQueryResponsibilitySegregation, "CQRS"}
    };

    public JavaScriptGenerator(
        IProjectStorageManager projectStorageManager,
        DatabaseConfigOptions? options,
        TemplateStorageOptions? templateStorageOptions,
        ProjectStorageOptions projectStorageOptions) : base(projectStorageManager, options, templateStorageOptions, projectStorageOptions)
    {}

    public override async Task<string?> GenerateAsync(DalGeneratorRequestDto model)
    {
        ProjectStorageManager.DeleteDalIfExists(model.ProjectId);
        var projectPath = ProjectStorageManager.GetProjectPath(model.ProjectId);
        var dalPath = Path.Combine(projectPath, ProjectStorageOptions.DalName);
        Directory.CreateDirectory(dalPath);

        var errors = await CommandExecutor.RunProcess("init -y", dalPath, "npm");
        if (errors is null)
        {
            var package = _packages[model.DataBaseId];
            errors = await CommandExecutor.RunProcess($"install sequelize sequelize-auto {package}", dalPath, "npm");
            if (errors is null)
            {
                var templateDalPath = Path.Combine(
                    Path.Combine(TemplateStorageOptions!.Path, $"{(LanguageEnum)model.LanguageId}"),
                    $"{_architectures[model.ArchitectureId]}");

                foreach (var file in Directory.EnumerateFiles(templateDalPath, "*", new EnumerationOptions {RecurseSubdirectories = true}))
                {
                    var relativePath = Path.GetRelativePath(templateDalPath, file);
                    var destPath = Path.Combine(dalPath, relativePath);
                    Directory.CreateDirectory(Path.GetDirectoryName(destPath) ?? string.Empty);
                    File.Copy(file, destPath, true);
                }

                var connectionString = ConnectionStringBuilder.GetConnectionStringJavaScript(
                    Options!.ConnectionStringParams,
                    (DataBaseEnum)model.DataBaseId,
                    $"{model.ProjectId}") ?? "";

                errors = await CommandExecutor.RunProcess(connectionString,dalPath,"npx");
            }
        }

        return errors;
    }
}