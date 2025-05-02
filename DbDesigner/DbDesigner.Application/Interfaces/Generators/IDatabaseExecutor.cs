namespace DbDesigner.Application.Interfaces.Generators;

public interface IDatabaseExecutor
{
    Task<string?> ValidateScriptAsync(string script, string dataBaseName);

    Task<bool> CreateDatabaseAsync(string dataBaseName);
}