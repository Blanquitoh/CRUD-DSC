using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;
using Refit;
using Sakila.Web.Abstractions;
using Sakila.Web.Common;
using Sakila.Web.Extensions;

namespace Sakila.Web.Services;

public abstract class BaseCrudService<TCreate, TUpdate, TGetAll, TGetById>(
    IValidator<TCreate> createValidator,
    IValidator<TUpdate> updateValidator)
{
    public EditContext EditContext { get; private set; } = null!;
    public ValidationMessageStore MessageStore { get; private set; } = null!;

    protected abstract int GetUpdateId(TUpdate request);

    protected abstract Task<TGetAll> GetAllApiAsync();
    protected abstract Task<TGetById?> GetByIdApiAsync(int id);
    protected abstract Task CreateApiAsync(TCreate request);
    protected abstract Task UpdateApiAsync(int id, TUpdate request);
    protected abstract Task DeleteApiAsync(int id);

    public void Initialize(object model)
    {
        EditContext = new EditContext(model);
        MessageStore = new ValidationMessageStore(EditContext);
    }

    public async Task GetAllAsync(
        Func<ISakilaApiResponse<TGetAll>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<TGetAll>, Task>? onFailure = null)
    {
        ISakilaApiResponse<TGetAll> sakilaApiResponse;
        try
        {
            var result = await GetAllApiAsync();
            sakilaApiResponse = new SakilaSakilaApiResponse<TGetAll>(result);
        }
        catch (ApiException exception)
        {
            sakilaApiResponse = await HandleApiException<TGetAll>(exception);
        }
        catch (HttpRequestException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<TGetAll>(exception.Message);
        }

        if (sakilaApiResponse.IsSuccess)
            if (onSuccess != null)
                await onSuccess(sakilaApiResponse);
        if (!sakilaApiResponse.IsSuccess)
            if (onFailure != null)
                await onFailure(sakilaApiResponse);
    }

    public async Task GetByIdAsync(int id,
        Func<ISakilaApiResponse<TGetById>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<TGetById>, Task>? onFailure = null)
    {
        ISakilaApiResponse<TGetById> sakilaApiResponse;
        try
        {
            var result = await GetByIdApiAsync(id);
            if (result == null)
                sakilaApiResponse = new SakilaSakilaApiResponse<TGetById>("Not found");
            else
                sakilaApiResponse = new SakilaSakilaApiResponse<TGetById>(result);
        }
        catch (ApiException exception)
        {
            sakilaApiResponse = await HandleApiException<TGetById>(exception);
        }
        catch (HttpRequestException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<TGetById>(exception.Message);
        }

        if (sakilaApiResponse.IsSuccess)
            if (onSuccess != null)
                await onSuccess(sakilaApiResponse);
        if (!sakilaApiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, sakilaApiResponse);
            if (onFailure != null)
                await onFailure(sakilaApiResponse);
        }
    }

    public async Task CreateAsync(TCreate request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        ISakilaApiResponse<object> sakilaApiResponse;
        try
        {
            await createValidator.ValidateAndThrowAsync(request);
            await CreateApiAsync(request);
            sakilaApiResponse = new SakilaSakilaApiResponse<object>();
        }
        catch (ValidationException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<object>(exception);
        }
        catch (ApiException exception)
        {
            sakilaApiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<object>(exception.Message);
        }

        if (sakilaApiResponse.IsSuccess)
            if (onSuccess != null)
                await onSuccess(sakilaApiResponse);
        if (!sakilaApiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, sakilaApiResponse);
            if (onFailure != null) await onFailure(sakilaApiResponse.Errors);
        }
    }

    public async Task UpdateAsync(TUpdate request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        ISakilaApiResponse<object> sakilaApiResponse;
        try
        {
            await updateValidator.ValidateAndThrowAsync(request);
            await UpdateApiAsync(GetUpdateId(request), request);
            sakilaApiResponse = new SakilaSakilaApiResponse<object>();
        }
        catch (ValidationException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<object>(exception);
        }
        catch (ApiException exception)
        {
            sakilaApiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<object>(exception.Message);
        }

        if (sakilaApiResponse.IsSuccess)
            if (onSuccess != null)
                await onSuccess(sakilaApiResponse);
        if (!sakilaApiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, sakilaApiResponse);
            if (onFailure != null) await onFailure(sakilaApiResponse.Errors);
        }
    }

    public async Task DeleteAsync(int id,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        ISakilaApiResponse<object> sakilaApiResponse;
        try
        {
            await DeleteApiAsync(id);
            sakilaApiResponse = new SakilaSakilaApiResponse<object>();
        }
        catch (ApiException exception)
        {
            sakilaApiResponse = await HandleApiException<object>(exception);
        }
        catch (HttpRequestException exception)
        {
            sakilaApiResponse = new SakilaSakilaApiResponse<object>(exception.Message);
        }

        if (sakilaApiResponse.IsSuccess)
            if (onSuccess != null)
                await onSuccess(sakilaApiResponse);

        if (!sakilaApiResponse.IsSuccess)
        {
            EditContext.ApplyErrors(MessageStore, sakilaApiResponse);
            if (onFailure != null)
                await onFailure(sakilaApiResponse.Errors);
        }
    }

    private static Task<SakilaSakilaApiResponse<T>> HandleApiException<T>(ApiException exception)
    {
        try
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            var document = JsonSerializer.Deserialize<JsonElement>(exception.Content!, options);
            if (exception.StatusCode == HttpStatusCode.BadRequest &&
                document.TryGetProperty("errors", out var errorsElement))
            {
                var errors =
                    JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText(), options)
                    ?? new Dictionary<string, string[]>();
                return Task.FromResult(new SakilaSakilaApiResponse<T>(errors));
            }

            if (document.TryGetProperty("detail", out var detail))
                return Task.FromResult(
                    new SakilaSakilaApiResponse<T>(detail.GetString() ?? $"Unexpected status: {exception.StatusCode}"));
        }
        catch (JsonException)
        {
            // ignore
        }

        return Task.FromResult(new SakilaSakilaApiResponse<T>($"Unexpected status: {exception.StatusCode}"));
    }
}