using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Services.Implementations;

public class CountryService(
    IApiClient apiClient,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator) : ICountryService
{
    private const string Resource = "countries";

    public async Task<IApiResponse<CountryGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<CountryGetAllResponse>(Resource);
    }

    public async Task<IApiResponse<CountryGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<CountryGetByIdResponse>($"{Resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(CountryCreateRequest request)
    {
        return await apiClient.PostAsync(Resource, request, createValidator);
    }

    public async Task<IApiResponse<object>> UpdateAsync(CountryUpdateRequest request)
    {
        return await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator);
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id)
    {
        return await apiClient.DeleteAsync($"{Resource}/{id}");
    }
}