using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Web.Extensions;

namespace Sakila.Web.Services.Implementations;

public class LanguageService(
    IApiClient apiClient,
    IValidator<LanguageCreateRequest> createValidator,
    IValidator<LanguageUpdateRequest> updateValidator) : ILanguageService
{
    private const string Resource = "languages";

    public EditContext EditContext { get; private set; } = null!;
    public ValidationMessageStore MessageStore { get; private set; } = null!;

    public void Initialize(object model)
    {
        EditContext = new EditContext(model);
        MessageStore = new ValidationMessageStore(EditContext);
    }

    public async Task GetAllAsync(
        Func<IApiResponse<LanguageGetAllResponse>, Task>? onSuccess = null,
        Func<IApiResponse<LanguageGetAllResponse>, Task>? onFailure = null)
    {
        await apiClient.GetAsync(Resource, onSuccess, onFailure);
    }

    public async Task GetByIdAsync(int id,
        Func<IApiResponse<LanguageGetByIdResponse>, Task>? onSuccess = null,
        Func<IApiResponse<LanguageGetByIdResponse>, Task>? onFailure = null)
    {
        await apiClient.GetAsync($"{Resource}/{id}", onSuccess, onFailure);
    }

    public async Task CreateAsync(LanguageCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PostAsync(Resource, request, createValidator,
            onSuccess,
            async response =>
            {
                EditContext.ApplyErrors(MessageStore, response);
                if (onFailure != null) await onFailure(response.Errors);
            });
    }

    public async Task UpdateAsync(LanguageUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator,
            onSuccess,
            async response =>
            {
                EditContext.ApplyErrors(MessageStore, response);
                if (onFailure != null) await onFailure(response.Errors);
            });
    }

    public async Task DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.DeleteAsync($"{Resource}/{id}",
            onSuccess,
            async response =>
            {
                if (onFailure != null) await onFailure(response.Errors);
            });
    }
}