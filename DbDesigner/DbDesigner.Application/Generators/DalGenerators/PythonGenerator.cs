using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators.DalGenerators;

public class PythonGenerator : BaseDalGenerator, IDalGenerator
{
    private readonly Dictionary<int, string> _packages = new()
    {
        {(int)DataBaseEnum.MySql, "mysql-connector-python"},
        {(int)DataBaseEnum.PostgreSql, "psycopg2-binary"},
        {(int)DataBaseEnum.SqLite, ""},
        {(int)DataBaseEnum.MsSql, "pyodbc"}
    };
    private readonly Dictionary<int, string> _architectures = new()
    {
        {(int)ArchitectureEnum.DirectDataAccess, "DDA"},
        {(int)ArchitectureEnum.RepositoryPattern, "Repository"},
        {(int)ArchitectureEnum.CommandQueryResponsibilitySegregation, "CQRS"}
    };

    public PythonGenerator(
        IProjectStorageManager projectStorageManager,
        DatabaseConfigOptions? options,
        TemplateStorageOptions? templateStorageOptions,
        ProjectStorageOptions projectStorageOptions) : base(projectStorageManager, options, templateStorageOptions, projectStorageOptions)
    {}

    public override async Task<string?> GenerateAsync(DalGeneratorRequestDto model)
    {
        ProjectStorageManager.DeleteDalIfExists(model.ProjectId);
        var projectPath = ProjectStorageManager.GetProjectPath(model.ProjectId);

        var errors = await CommandExecutor.RunProcess($"-m venv {ProjectStorageOptions.DalName}", projectPath, "python3");
        if (errors is null)
        {
            var package = _packages[model.DataBaseId];
            errors = await CommandExecutor.RunProcess($"-c \"source {ProjectStorageOptions.DalName}/bin/activate && pip install sqlalchemy sqlacodegen {package}\"", projectPath, "/bin/bash");

            if (errors is null)
            {
                var dalPath = Path.Combine(projectPath, ProjectStorageOptions.DalName);
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
                
                var connectionString = ConnectionStringBuilder.GetConnectionStringPython(
                    Options!.ConnectionStringParams,
                    (DataBaseEnum)model.DataBaseId,
                    $"{model.ProjectId}") ?? "";
                
                errors = await CommandExecutor.RunProcess($"-c \"source bin/activate && {connectionString}\"",dalPath,"/bin/bash");
            }
        }

        return errors;
    }
}