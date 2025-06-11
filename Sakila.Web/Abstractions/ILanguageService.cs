using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Abstractions;

public interface ILanguageService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);
    Task GetAllAsync(
        Func<IApiResponse<LanguageGetAllResponse>, Task>? onSuccess = null,
        Func<IApiResponse<LanguageGetAllResponse>, Task>? onFailure = null);
    Task GetByIdAsync(int id,
        Func<IApiResponse<LanguageGetByIdResponse>, Task>? onSuccess = null,
        Func<IApiResponse<LanguageGetByIdResponse>, Task>? onFailure = null);
    Task CreateAsync(LanguageCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task UpdateAsync(LanguageUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
    Task DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}