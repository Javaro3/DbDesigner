using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Common.GenerateModels;
using Service.Interfaces.Infrastructure.Infrastructure;

namespace Infrastructure.TcpManagers;

public class TcpManager : ITcpManager
{
    public async Task<string> SendGenerationDataAsync(DataProjectGenerateModel model)
    {
        var jsonData = JsonSerializer.Serialize(model);
        using var client = new TcpClient("127.0.0.1", 9999);
        await using var stream = client.GetStream();

        var data = Encoding.UTF8.GetBytes(jsonData);

        var dataSize = BitConverter.GetBytes(data.Length);
        await stream.WriteAsync(dataSize, 0, dataSize.Length);
        await stream.WriteAsync(data, 0, data.Length);

        var responseSizeBytes = new byte[4];
        await stream.ReadAsync(responseSizeBytes, 0, responseSizeBytes.Length);
        var responseSize = BitConverter.ToInt32(responseSizeBytes, 0);

        var responseBytes = new byte[responseSize];
        var bytesRead = 0;
        while (bytesRead < responseSize)
        {
            bytesRead += await stream.ReadAsync(responseBytes, bytesRead, responseSize - bytesRead);
        }

        var response = Encoding.UTF8.GetString(responseBytes);
        return response;
    }
}