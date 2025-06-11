using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Abstractions;

public interface ICountryService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);
    Task<IApiResponse<CountryGetAllResponse>> GetAllAsync();
    Task<IApiResponse<CountryGetByIdResponse>> GetByIdAsync(int id);
    Task<IApiResponse<object>> CreateAsync(CountryCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task<IApiResponse<object>> UpdateAsync(CountryUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task<IApiResponse<object>> DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}