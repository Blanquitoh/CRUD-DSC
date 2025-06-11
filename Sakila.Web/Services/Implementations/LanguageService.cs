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

    public async Task<IApiResponse<LanguageGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<LanguageGetAllResponse>(Resource);
    }

    public async Task<IApiResponse<LanguageGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<LanguageGetByIdResponse>($"{Resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(LanguageCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.PostAsync(Resource, request, createValidator);

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            EditContext.ApplyErrors(MessageStore, response);
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }

    public async Task<IApiResponse<object>> UpdateAsync(LanguageUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator);

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            EditContext.ApplyErrors(MessageStore, response);
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.DeleteAsync($"{Resource}/{id}");

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }
}