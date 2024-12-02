using System.IO.Compression;
using Common.GenerateModels;
using Common.Options;
using Microsoft.Extensions.Options;
using Service.Interfaces.Infrastructure.DataServices;

namespace Service.DataServices;

public class ProjectStorageDataService : IProjectStorageDataService
{
    private readonly string _path;

    public ProjectStorageDataService(IOptions<ProjectStorageOptions> options)
    {
        _path = options.Value.Path;
            
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

    public async Task SaveGenerationResultModel(ResultGenerateModel model)
    {
        var baseDirectory = Path.Combine(_path, model.ProjectId.ToString());
        Directory.CreateDirectory(baseDirectory);

        var sqlCreatePath = Path.Combine(baseDirectory, "database.sql");
        await File.WriteAllTextAsync(sqlCreatePath, model.CreateScript);

        var domainDirectory = Path.Combine(baseDirectory, "Domains");
        Directory.CreateDirectory(domainDirectory);
        foreach (var domain in model.Domains)
        {
            var domainPath = Path.Combine(domainDirectory, $"{domain.DomainName}.cs");
            await File.WriteAllTextAsync(domainPath, domain.DomainContent);
        }
        
        var archivePath = Path.Combine(_path, $"{model.ProjectId}.zip");
        if (File.Exists(archivePath))
        {
            File.Delete(archivePath);
        }
        ZipFile.CreateFromDirectory(baseDirectory, archivePath);
        Directory.Delete(baseDirectory, true);
    }
    
    public async Task<byte[]> GetProject(int projectId)
    {
        var filePath = Path.Combine(_path, $"{projectId}.zip");

        if (!File.Exists(filePath))
            throw new Exception("Project not found.");

        var fileBytes = await File.ReadAllBytesAsync(filePath);
        return fileBytes;
    }
}