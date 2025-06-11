using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Api;

namespace Sakila.Web.Services.Implementations;

public class CountryService(
    ICountriesApi api,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator)
    : BaseCrudService<CountryCreateRequest, CountryUpdateRequest, CountryGetAllResponse, CountryGetByIdResponse>(
        createValidator, updateValidator), ICountryService
{
    protected override int GetUpdateId(CountryUpdateRequest request) => request.Id;

    protected override Task<CountryGetAllResponse> GetAllApiAsync() => api.GetAllAsync();

    protected override Task<CountryGetByIdResponse?> GetByIdApiAsync(int id) => api.GetByIdAsync(id);

    protected override Task CreateApiAsync(CountryCreateRequest request) => api.PostAsync(request);

    protected override Task UpdateApiAsync(int id, CountryUpdateRequest request) => api.PutAsync(id, request);

    protected override Task DeleteApiAsync(int id) => api.DeleteAsync(id);
}
