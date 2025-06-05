using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Services;

public class CountryService(IApiClient apiClient) : ICountryService
{
    private readonly string _resource = "countries";

    public async Task<CountryGetAllResponse> GetAllAsync()
    {
        return await apiClient.GetAsync<CountryGetAllResponse>(_resource);
    }

    public async Task<CountryGetByIdResponse> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<CountryGetByIdResponse>($"{_resource}/{id}");
    }

    public async Task CreateAsync(CountryCreateRequest request)
    {
        await apiClient.PostAsync(_resource, request);
    }

    public async Task UpdateAsync(CountryUpdateRequest request)
    {
        await apiClient.PutAsync($"{_resource}/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}