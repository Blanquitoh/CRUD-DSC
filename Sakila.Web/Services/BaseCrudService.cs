using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Web.Abstractions;
using Sakila.Web.Extensions;

namespace Sakila.Web.Services;

public abstract class BaseCrudService<TCreate, TUpdate, TGetAll, TGetById>(
    string resource,
    IApiClient apiClient,
    IValidator<TCreate> createValidator,
    IValidator<TUpdate> updateValidator)
{
    protected abstract int GetUpdateRequestId(TUpdate request);

    public EditContext EditContext { get; private set; } = null!;
    public ValidationMessageStore MessageStore { get; private set; } = null!;

    public void Initialize(object model)
    {
        EditContext = new EditContext(model);
        MessageStore = new ValidationMessageStore(EditContext);
    }

    public async Task GetAllAsync(
        Func<IApiResponse<TGetAll>, Task>? onSuccess = null,
        Func<IApiResponse<TGetAll>, Task>? onFailure = null)
    {
        await apiClient.GetAsync(resource, onSuccess, onFailure);
    }

    public async Task GetByIdAsync(int id,
        Func<IApiResponse<TGetById>, Task>? onSuccess = null,
        Func<IApiResponse<TGetById>, Task>? onFailure = null)
    {
        await apiClient.GetAsync($"{resource}/{id}", onSuccess, onFailure);
    }

    public async Task CreateAsync(TCreate request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PostAsync(resource, request, createValidator,
            onSuccess,
            async response =>
            {
                EditContext.ApplyErrors(MessageStore, response);
                if (onFailure != null) await onFailure(response.Errors);
            });
    }

    public async Task UpdateAsync(TUpdate request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PutAsync($"{resource}/{GetUpdateRequestId(request)}", request, updateValidator,
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
        await apiClient.DeleteAsync($"{resource}/{id}",
            onSuccess,
            async response =>
            {
                if (onFailure != null) await onFailure(response.Errors);
            });
    }
}