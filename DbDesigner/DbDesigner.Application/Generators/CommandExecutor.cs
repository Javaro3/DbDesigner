using System.Diagnostics;

namespace DbDesigner.Application.Generators;

public static class CommandExecutor
{
    public static async Task<string?> RunProcess(string arguments, string workingDirectory, string executor)
    {
        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = executor,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        process.Start();
        var error = await process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync();
        return process.ExitCode != 0 ? error : null;
    }
}