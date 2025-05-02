using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;

namespace DbDesigner.Application.Generators.DalGenerators;

public class CSharpGenerator : BaseDalGenerator, IDalGenerator
{
    private readonly Dictionary<int, (string Name, string Version)> _packages = new()
    {
        {(int)DataBaseEnum.MySql, ("Pomelo.EntityFrameworkCore.MySql", "8.0.3")},
        {(int)DataBaseEnum.PostgreSql, ("Npgsql.EntityFrameworkCore.PostgreSQL", "8.0.11")},
        {(int)DataBaseEnum.SqLite, ("Microsoft.EntityFrameworkCore.Sqlite", "8.0.14")},
        {(int)DataBaseEnum.MsSql, ("Microsoft.EntityFrameworkCore.SqlServer", "8.0.14")}
    };
    private readonly Dictionary<int, string> _architectures = new()
    {
        {(int)ArchitectureEnum.DirectDataAccess, "dda"},
        {(int)ArchitectureEnum.RepositoryPattern, "repo"},
        {(int)ArchitectureEnum.CommandQueryResponsibilitySegregation, "cqrs"}
    };

    public CSharpGenerator(
        IProjectStorageManager projectStorageManager,
        DatabaseConfigOptions? options,
        TemplateStorageOptions? templateStorageOptions,
        ProjectStorageOptions projectStorageOptions) : base(projectStorageManager, options, templateStorageOptions, projectStorageOptions)
    {}

    public override async Task<string?> GenerateAsync(DalGeneratorRequestDto model)
    {
        ProjectStorageManager.DeleteDalIfExists(model.ProjectId);
        var projectPath = ProjectStorageManager.GetProjectPath(model.ProjectId);

        var errors = await CommandExecutor.RunProcess(
            $"new {_architectures[model.ArchitectureId]} -n {ProjectStorageOptions.DalName}",
            projectPath,
            "dotnet");

        if (errors is null)
        {
            var dalPath = Path.Combine(projectPath, ProjectStorageOptions.DalName);
            var package = _packages[model.DataBaseId];
            errors = await CommandExecutor.RunProcess(
                $"add package {package.Name} --version {package.Version}",
                dalPath,
                "dotnet");

            if (errors is null)
            {
                var connectionString = ConnectionStringBuilder.GetConnectionStringCSharp(
                    Options!.ConnectionStringParams,
                    (DataBaseEnum)model.DataBaseId,
                    model.ProjectId.ToString());

                errors = await CommandExecutor.RunProcess(
                    $"ef dbcontext scaffold \"{connectionString}\" {package.Name} --context ApplicationDbContext --context-dir Data --output-dir Models --no-onconfiguring",
                    dalPath,
                    "dotnet");
            }
        }

        return errors;
    }
}