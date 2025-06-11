using Microsoft.AspNetCore.Components.Forms;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Abstractions;

public interface ICountryService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);

    Task GetAllAsync(
        Func<ISakilaApiResponse<CountryGetAllResponse>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<CountryGetAllResponse>, Task>? onFailure = null);

    Task GetByIdAsync(int id,
        Func<ISakilaApiResponse<CountryGetByIdResponse>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<CountryGetByIdResponse>, Task>? onFailure = null);

    Task CreateAsync(CountryCreateRequest request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task UpdateAsync(CountryUpdateRequest request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task DeleteAsync(int id,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}