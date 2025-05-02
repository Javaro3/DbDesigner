namespace DbDesigner.Application.Interfaces.Generators;

public interface IProjectStorageManager
{
    Task SaveScriptAsync(string script, int projectId);
    
    Task SaveDataScriptAsync(string script, int projectId);
    
    Task<string?> GetScriptAsync(int projectId);
    
    string GetProjectPath(int projectId);
    
    void DeleteDalIfExists(int projectId);
    
    Task<byte[]> GetProjectFilesAsync(int projectId);
}
