using Microsoft.AspNetCore.Components.Forms;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Web.Abstractions;

public interface ILanguageService
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);

    Task GetAllAsync(
        Func<ISakilaApiResponse<LanguageGetAllResponse>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<LanguageGetAllResponse>, Task>? onFailure = null);

    Task GetByIdAsync(int id,
        Func<ISakilaApiResponse<LanguageGetByIdResponse>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<LanguageGetByIdResponse>, Task>? onFailure = null);

    Task CreateAsync(LanguageCreateRequest request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task UpdateAsync(LanguageUpdateRequest request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task DeleteAsync(int id,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}