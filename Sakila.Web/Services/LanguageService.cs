using System.Net.Http.Json;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;

namespace Sakila.Web.Services;

public class LanguageService(HttpClient httpClient) : ILanguageService
{
    public async Task<LanguageGetAllResponse> GetAllAsync()
    {
        var response = await httpClient.GetAsync("api/languages");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LanguageGetAllResponse>() ?? new LanguageGetAllResponse();
    }

    public async Task<LanguageGetByIdResponse> GetByIdAsync(int id)
    {
        var response = await httpClient.GetAsync($"api/languages/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<LanguageGetByIdResponse>() ?? new LanguageGetByIdResponse();
    }

    public async Task<int> CreateAsync(LanguageCreateRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("api/languages", request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task UpdateAsync(LanguageUpdateRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"api/languages/{request.Id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"api/languages/{id}");
        response.EnsureSuccessStatusCode();
    }
}