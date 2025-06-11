using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using System.Net;
using System.Text.Json;
using Sakila.Web.Abstractions;
using Sakila.Web.Common;
using Sakila.Web.Extensions;

namespace Sakila.Web.Services;

public abstract class BaseCrudService<TCreate, TUpdate, TGetAll, TGetById>(
    IValidator<TCreate> createValidator,
    IValidator<TUpdate> updateValidator)
{
    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    protected abstract int GetUpdateId(TUpdate request);

    protected abstract Task<TGetAll> GetAllApiAsync();
    protected abstract Task<TGetById?> GetByIdApiAsync(int id);
    protected abstract Task CreateApiAsync(TCreate request);
    protected abstract Task UpdateApiAsync(int id, TUpdate request);
    protected abstract Task DeleteApiAsync(int id);

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
        IApiResponse<TGetAll> apiResponse;
        try
        {
            var result = await GetAllApiAsync();
            apiResponse = new SakilaApiResponse<TGetAll>(result);
        }
        catch (Refit.ApiException exception)
        {
            apiResponse = await HandleApiException<TGetAll>(exception);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new SakilaApiResponse<TGetAll>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    public async Task GetByIdAsync(int id,
        Func<IApiResponse<TGetById>, Task>? onSuccess = null,
        Func<IApiResponse<TGetById>, Task>? onFailure = null)
    {
        IApiResponse<TGetById> apiResponse;
        try
        {
            var result = await GetByIdApiAsync(id);
            if (result == null)
                apiResponse = new SakilaApiResponse<TGetById>("Not found");
            else
                apiResponse = new SakilaApiResponse<TGetById>(result);
        }
        catch (Refit.ApiException exception)
        {
            apiResponse = await HandleApiException<TGetById>(exception);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new SakilaApiResponse<TGetById>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    public async Task CreateAsync(TCreate request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            await createValidator.ValidateAndThrowAsync(request);
            await CreateApiAsync(request);
            apiResponse = new SakilaApiResponse<object>();
        }
        catch (ValidationException exception)
        {
            apiResponse = new SakilaApiResponse<object>(exception);
        }
        catch (Refit.ApiException exception)
        {
            apiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new SakilaApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, apiResponse);
            if (onFailure != null) await onFailure(apiResponse.Errors);
        }
    }

    public async Task UpdateAsync(TUpdate request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            await updateValidator.ValidateAndThrowAsync(request);
            await UpdateApiAsync(GetUpdateId(request), request);
            apiResponse = new SakilaApiResponse<object>();
        }
        catch (ValidationException exception)
        {
            apiResponse = new SakilaApiResponse<object>(exception);
        }
        catch (Refit.ApiException exception)
        {
            apiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new SakilaApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, apiResponse);
            if (onFailure != null) await onFailure(apiResponse.Errors);
        }
    }

    public async Task DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            await DeleteApiAsync(id);
            apiResponse = new SakilaApiResponse<object>();
        }
        catch (Refit.ApiException exception)
        {
            apiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new SakilaApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
        {
            if (onFailure != null) await onFailure(apiResponse.Errors);
        }
    }

    private static Task<SakilaApiResponse<T>> HandleApiException<T>(Refit.ApiException exception)
    {
        try
        {
            var document = JsonSerializer.Deserialize<JsonElement>(exception.Content!, Options);
            if (exception.StatusCode == HttpStatusCode.BadRequest &&
                document.TryGetProperty("errors", out var errorsElement))
            {
                var errors =
                    JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText(), Options)
                    ?? new Dictionary<string, string[]>();
                return Task.FromResult(new SakilaApiResponse<T>(errors));
            }

            if (document.TryGetProperty("detail", out var detail))
                return Task.FromResult(new SakilaApiResponse<T>(detail.GetString() ?? $"Unexpected status: {exception.StatusCode}"));
        }
        catch (JsonException)
        {
            // ignore
        }

        return Task.FromResult(new SakilaApiResponse<T>($"Unexpected status: {exception.StatusCode}"));
    }
}