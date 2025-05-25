using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Services;

public class LanguageService(IApiClient apiClient) : ILanguageService
{
    private readonly string _resource = "languages";

    public async Task<LanguageGetAllResponse> GetAllAsync()
    {
        return await apiClient.GetAsync<LanguageGetAllResponse>(_resource);
    }

    public async Task<LanguageGetByIdResponse> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<LanguageGetByIdResponse>($"{_resource}/{id}");
    }

    public async Task CreateAsync(LanguageCreateRequest request)
    {
        await apiClient.PostAsync(_resource, request);
    }

    public async Task UpdateAsync(LanguageUpdateRequest request)
    {
        await apiClient.PutAsync($"{_resource}/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}