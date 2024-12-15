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

        if (model.DataScript is not null)
        {
            var sqlDataPath = Path.Combine(baseDirectory, "data.sql");
            await File.WriteAllTextAsync(sqlDataPath, model.DataScript);
        }

        var dalDirectory = Path.Combine(baseDirectory, "DAL");
        
        var domainDirectory = Path.Combine(dalDirectory, "Domains");
        Directory.CreateDirectory(domainDirectory);
        foreach (var domain in model.Domains)
        {
            var domainPath = Path.Combine(domainDirectory, $"{domain.DomainName}.cs");
            await File.WriteAllTextAsync(domainPath, domain.DomainContent);
        }

        var ormPath = Path.Combine(dalDirectory, $"{model.Orm.OrmName}.cs");
        await File.WriteAllTextAsync(ormPath, model.Orm.OrmContect);

        if (model.Architecture is not null)
        {
            var architecturePath = Path.Combine(dalDirectory, model.Architecture.Name);
            Directory.CreateDirectory(architecturePath);
            if (model.Architecture.InterfaceFiles.Any())
            {
                var architectureInterfacePath = Path.Combine(architecturePath, "Interfaces");
                Directory.CreateDirectory(architectureInterfacePath);

                foreach (var (name, content) in model.Architecture.InterfaceFiles)
                {
                    var interfacePath = Path.Combine(architectureInterfacePath, $"{name}.cs");
                    await File.WriteAllTextAsync(interfacePath, content);
                }
            }
            
            if (model.Architecture.ImplementationFiles.Any())
            {
                var architectureImplementationPath = Path.Combine(architecturePath, "Implementations");
                Directory.CreateDirectory(architectureImplementationPath);

                foreach (var (name, content) in model.Architecture.ImplementationFiles)
                {
                    var implementPath = Path.Combine(architectureImplementationPath, $"{name}.cs");
                    await File.WriteAllTextAsync(implementPath, content);
                }
            }
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