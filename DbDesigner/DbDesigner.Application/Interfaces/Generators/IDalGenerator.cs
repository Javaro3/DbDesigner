using DbDesigner.Application.Dtos.Generate;

namespace DbDesigner.Application.Interfaces.Generators;

public interface IDalGenerator
{
    public Task<string?> GenerateAsync(DalGeneratorRequestDto model);
}