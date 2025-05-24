using System.Net.Http.Json;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;

namespace Sakila.Web.Services;

public class LanguageService(HttpClient httpClient) : ILanguageService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<LanguageGetAllResponse> GetAllAsync()
    {
        var response = await _httpClient.GetAsync("api/languages");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LanguageGetAllResponse> () ?? new();
    }

    public async Task<LanguageGetByIdResponse> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/languages/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LanguageGetByIdResponse>() ?? new();
    }

    public async Task<int> CreateAsync(LanguageCreateRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/languages", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateAsync(LanguageUpdateRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/languages/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/languages/{id}");
        response.EnsureSuccessStatusCode();
    }
}