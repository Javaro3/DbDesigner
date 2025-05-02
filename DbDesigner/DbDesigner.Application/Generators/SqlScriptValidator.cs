using DbDesigner.Application.Generators.DatabaseExecutors;
using DbDesigner.Application.Interfaces.DockerServices;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Enums;
using DbDesigner.Domain.Options;
using Microsoft.Extensions.Options;

namespace DbDesigner.Application.Generators;

public class SqlScriptValidator : ISqlScriptValidator
{
    private readonly IDockerManager _dockerManager;
    private readonly DatabaseOptions _options;

    public SqlScriptValidator(
        IDockerManager dockerManager,
        IOptions<DatabaseOptions> options)
    {
        _dockerManager = dockerManager;
        _options = options.Value;
    }
    
    public async Task<string?> ValidateScriptAsync(string script, string dataBaseName, DataBaseEnum? dataBase)
    {
        try
        {
            var dataBaseOptions = GetDataBaseConfigOptions(dataBase);

            if (!await _dockerManager.DockerContainerExist(dataBaseOptions!.ContainerName))
                await _dockerManager.RunDockerComposeAsync(dataBaseOptions.ComposePath);

            var databaseExecutor = GetDatabaseExecutor(dataBase, dataBaseOptions);
            var databaseIsCreated = await databaseExecutor.CreateDatabaseAsync(dataBaseName);

            return databaseIsCreated
                ? await databaseExecutor.ValidateScriptAsync(script, dataBaseName)
                : "Error during script validation. Try again later";
        }
        catch (Exception ex)
        {
            return ex.Message;
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

    private static IDatabaseExecutor GetDatabaseExecutor(DataBaseEnum? dataBase, DatabaseConfigOptions? options)
    {
        return dataBase switch
        {
            DataBaseEnum.MySql => new MySqlExecutor(options),
            DataBaseEnum.PostgreSql => new PostgreSqlExecutor(options),
            DataBaseEnum.SqLite => new SqLiteExecutor(options),
            DataBaseEnum.MsSql => new MsSqlExecutor(options),
            _ => throw new ArgumentException("Database is not supported")
        };
    }
}