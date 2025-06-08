using FluentValidation;
using Sakila.Web.Abstractions;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Common;

namespace Sakila.Web.Services.Implementations;

public class CountryService(
    IApiClient apiClient,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator) : ICountryService
{
    private readonly string _resource = "countries";

    public async Task<IApiResponse<CountryGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<CountryGetAllResponse>(_resource);
    }

    public async Task<IApiResponse<CountryGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<CountryGetByIdResponse>($"{_resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(CountryCreateRequest request)
    {
        return await apiClient.PostAsync(_resource, request, createValidator);
    }

    public async Task<IApiResponse<object>> UpdateAsync(CountryUpdateRequest request)
    {
        return await apiClient.PutAsync($"{_resource}/{request.Id}", request, updateValidator);
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id)
    {
        return await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}