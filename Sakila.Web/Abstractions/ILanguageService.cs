using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Abstractions;

public interface ILanguageService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);
    Task<IApiResponse<LanguageGetAllResponse>> GetAllAsync();
    Task<IApiResponse<LanguageGetByIdResponse>> GetByIdAsync(int id);
    Task<IApiResponse<object>> CreateAsync(LanguageCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task<IApiResponse<object>> UpdateAsync(LanguageUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task<IApiResponse<object>> DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}