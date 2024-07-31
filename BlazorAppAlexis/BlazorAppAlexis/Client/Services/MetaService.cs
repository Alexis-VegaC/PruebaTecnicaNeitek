using System.Net.Http.Json;
using BlazorAppAlexis.Shared;
using BlazorAppAlexis.Shared.Dtos;

namespace BlazorAppAlexis.Client.Services;

public class MetaService
{
    private readonly HttpClient _httpClient;

    public MetaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<MetaDto>> GetMetasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<MetaDto>>("api/metas");
    }

    public async Task<MetaDto> GetMetaAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<MetaDto>($"api/metas/{id}");
    }

    public async Task CreateMetaAsync(MetaDto meta)
    {
        await _httpClient.PostAsJsonAsync("api/metas", meta);
    }

    public async Task UpdateMetaAsync(MetaDto meta)
    {
        await _httpClient.PutAsJsonAsync($"api/metas/{meta.Id}", meta);
    }

    public async Task DeleteMetaAsync(int id)
    {
        await _httpClient.DeleteAsync($"api/metas/{id}");
    }
}