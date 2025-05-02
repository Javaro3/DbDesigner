using DbDesigner.Application.Dtos.Generate;

namespace DbDesigner.Application.Interfaces.Generators;

public interface IDalGeneratorManager
{
    Task<string?> GenerateAsync(DalGeneratorRequestDto model);
}