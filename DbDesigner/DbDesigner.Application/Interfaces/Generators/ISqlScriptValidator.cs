using DbDesigner.Domain.Enums;

namespace DbDesigner.Application.Interfaces.Generators;

public interface ISqlScriptValidator
{
    public Task<string?> ValidateScriptAsync(string script, string dataBaseName, DataBaseEnum? dataBase);
}