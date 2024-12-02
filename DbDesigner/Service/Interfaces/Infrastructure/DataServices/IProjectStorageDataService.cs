using Common.GenerateModels;

namespace Service.Interfaces.Infrastructure.DataServices;

public interface IProjectStorageDataService
{
    Task SaveGenerationResultModel(ResultGenerateModel model);

    Task<byte[]> GetProject(int projectId);
}