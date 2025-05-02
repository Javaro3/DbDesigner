using System.Diagnostics;
using DbDesigner.Application.Interfaces.DockerServices;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace DbDesigner.Application.DockerServices;

public class DockerManager : IDockerManager
{
    public async Task<bool> DockerContainerExist(string containerName)
    {
        using var client = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
        var containers = await client.Containers.ListContainersAsync(new ContainersListParameters { All = true });
        return containers.Any(e => e.Names.Contains($"/{containerName}") && e.State == "running");
    }
    
    public async Task<bool> RunDockerComposeAsync(string composeFile)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "docker-compose",
                Arguments = $"-f {composeFile} up -d",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        await process.WaitForExitAsync();
        return process.ExitCode == 0;
    }
}