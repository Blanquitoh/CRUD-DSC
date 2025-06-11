using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Services.Implementations;

public class CountryService(
    IApiClient apiClient,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator)
    : BaseCrudService<CountryCreateRequest, CountryUpdateRequest, CountryGetAllResponse, CountryGetByIdResponse>(
        "countries", apiClient, createValidator, updateValidator), ICountryService
{
    protected override int GetUpdateRequestId(CountryUpdateRequest request) => request.Id;
}
