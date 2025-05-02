using System.IO.Compression;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Options;
using Microsoft.Extensions.Options;

namespace DbDesigner.Application.Generators;

public class ProjectStorageManager : IProjectStorageManager
{
    private readonly ProjectStorageOptions _projectStorageOptions;

    public ProjectStorageManager(IOptions<ProjectStorageOptions> projectStorageOptions)
    {
        _projectStorageOptions = projectStorageOptions.Value;
    }

    public async Task SaveScriptAsync(string script, int projectId)
    {
        await SaveFileAsync(script, projectId, _projectStorageOptions.SqlScriptFileName);
    }
    
    public async Task SaveDataScriptAsync(string script, int projectId)
    {
        await SaveFileAsync(script, projectId, _projectStorageOptions.DataScriptFileName);
    }

    public async Task<string?> GetScriptAsync(int projectId)
    {
        var projectScriptPath = Path.Combine(
            GetProjectPath(projectId),
            _projectStorageOptions.SqlScriptFileName);
        return await File.ReadAllTextAsync(projectScriptPath);
    }

    public string GetProjectPath(int projectId)
    {
        return Path.Combine(_projectStorageOptions.Path, projectId.ToString());
    }

    public void DeleteDalIfExists(int projectId)
    {
        var path = Path.Combine(GetProjectPath(projectId), _projectStorageOptions.DalName);
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    public async Task<byte[]> GetProjectFilesAsync(int projectId)
    {
        var projectDirectoryPath = GetProjectPath(projectId);
        
        using var memoryStream = new MemoryStream();
    
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            foreach (var filePath in Directory.EnumerateFiles(projectDirectoryPath, "*", SearchOption.AllDirectories))
            {
                var relativePath = Path.GetRelativePath(projectDirectoryPath, filePath);
                var entry = archive.CreateEntry(relativePath);

                await using var entryStream = entry.Open();
                await using var fileStream = File.OpenRead(filePath);
                await fileStream.CopyToAsync(entryStream);
            }
        }
    
        return memoryStream.ToArray();
    }

    private async Task SaveFileAsync(string script, int projectId, string fileName)
    {
        var projectDirectoryPath = GetProjectPath(projectId);

        if (!Directory.Exists(projectDirectoryPath))
        {
            Directory.CreateDirectory(projectDirectoryPath);
        }

        var projectScriptPath = Path.Combine(projectDirectoryPath, fileName);
        await File.WriteAllTextAsync(projectScriptPath, script);
    }
}