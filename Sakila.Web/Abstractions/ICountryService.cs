using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Abstractions;

public interface ICountryService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);
    Task GetAllAsync(
        Func<IApiResponse<CountryGetAllResponse>, Task>? onSuccess = null,
        Func<IApiResponse<CountryGetAllResponse>, Task>? onFailure = null);
    Task GetByIdAsync(int id,
        Func<IApiResponse<CountryGetByIdResponse>, Task>? onSuccess = null,
        Func<IApiResponse<CountryGetByIdResponse>, Task>? onFailure = null);
    Task CreateAsync(CountryCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task UpdateAsync(CountryUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}