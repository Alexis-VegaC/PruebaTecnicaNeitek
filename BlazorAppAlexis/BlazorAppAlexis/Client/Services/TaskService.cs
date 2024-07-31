using System.Net.Http.Json;
using BlazorAppAlexis.Shared.Dtos;

namespace BlazorAppAlexis.Client.Services;

public class TaskService
{
    private readonly HttpClient _httpClient;

    public TaskService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    
    public async Task<List<TareaDto>> GetAllTareasAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<TareaDto>>("api/tasks");
    }

    public async Task<List<TareaDto>> GetTareasByMetaIdAsync(int metaId)
    {
        return await _httpClient.GetFromJsonAsync<List<TareaDto>>($"api/tasks/meta/{metaId}");
    }

    public async Task<TareaDto> GetTareaAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<TareaDto>($"api/tasks/{id}");
    }

    public async Task CreateTaskAsync(TareaDto tareaDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/tasks", tareaDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateTaskAsync(TareaDto tareaDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/tasks/{tareaDto.Id}", tareaDto);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTaskAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
        response.EnsureSuccessStatusCode();
    }
    
    public async Task CompleteTaskAsync(int id)
    {
        var response = await _httpClient.PutAsync($"api/tasks/complete/{id}", null);
        response.EnsureSuccessStatusCode();
    }
}