namespace DbDesigner.Application.Interfaces.DockerServices;

public interface IDockerManager
{
    Task<bool> DockerContainerExist(string containerName);

    Task<bool> RunDockerComposeAsync(string composeFile);
}