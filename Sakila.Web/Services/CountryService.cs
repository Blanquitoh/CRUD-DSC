using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Services;

public class CountryService(
    IApiClient apiClient,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator,
    IValidator<CountryDeleteRequest> deleteValidator) : ICountryService
{
    private readonly string _resource = "countries";
    private readonly IValidator<CountryCreateRequest> _createValidator = createValidator;
    private readonly IValidator<CountryUpdateRequest> _updateValidator = updateValidator;
    private readonly IValidator<CountryDeleteRequest> _deleteValidator = deleteValidator;

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
        await _createValidator.ValidateAndThrowAsync(request);
        await apiClient.PostAsync(_resource, request);
    }

    public async Task UpdateAsync(CountryUpdateRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);
        await apiClient.PutAsync($"{_resource}/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        var request = new CountryDeleteRequest { Id = id };
        await _deleteValidator.ValidateAndThrowAsync(request);
        await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}