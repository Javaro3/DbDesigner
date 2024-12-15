using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.Infrastructure;

public interface ITcpManager
{
    Task<string> SendGenerationDataAsync(DataProjectGenerateModel model);
}