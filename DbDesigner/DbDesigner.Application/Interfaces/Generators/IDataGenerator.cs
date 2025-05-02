using DbDesigner.Application.Dtos.Generate;

namespace DbDesigner.Application.Interfaces.Generators;

public interface IDataGenerator
{
    Task<string?> GenerateAsync(DataGeneratorRequestDto model);
}