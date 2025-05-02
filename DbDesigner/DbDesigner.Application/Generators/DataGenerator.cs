using System.Net.Http.Json;
using DbDesigner.Application.Dtos.Generate;
using DbDesigner.Application.Interfaces.Generators;
using DbDesigner.Domain.Options;
using Microsoft.Extensions.Options;

namespace DbDesigner.Application.Generators;

public class DataGenerator : IDataGenerator
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly DataGeneratorServiceOptions _dataGeneratorServiceOptions;
    private readonly IProjectStorageManager _projectStorageManager;
    
    public DataGenerator(
        IHttpClientFactory httpClientFactory,
        IOptions<DataGeneratorServiceOptions> dataGeneratorServiceOptions,
        IProjectStorageManager projectStorageManager)
    {
        _httpClientFactory = httpClientFactory;
        _projectStorageManager = projectStorageManager;
        _dataGeneratorServiceOptions = dataGeneratorServiceOptions.Value;
    }
    
    public async Task<string?> GenerateAsync(DataGeneratorRequestDto model)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("PythonDataGenerator");
            var url = $"{_dataGeneratorServiceOptions.Url}/generate";
            var response = await httpClient.PostAsJsonAsync(url, model);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<DataGeneratorResponseDto>();

            if (!string.IsNullOrEmpty(result?.Script))
            {
                await _projectStorageManager.SaveDataScriptAsync(result.Script, model.Project!.Id);
            }
            
            return result?.Errors;
        }
        catch (HttpRequestException)
        {
            return "Failed to generate data: service unavailable";
        }
        catch (Exception)
        {
            return "Unexpected error during data generation";
        }
    }
}