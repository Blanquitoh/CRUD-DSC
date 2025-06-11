using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentValidation;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Common;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private const string Base = "api/";

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    public async Task GetAsync<TResponse>(string url,
        Func<IApiResponse<TResponse>, Task>? onSuccess = null,
        Func<IApiResponse<TResponse>, Task>? onFailure = null)
    {
        IApiResponse<TResponse> apiResponse;
        try
        {
            var response = await httpClient.GetAsync($"{Base}{url}");
            apiResponse = await HandleResponse<TResponse>(response);
        }
        catch (ApiValidationException exception)
        {
            apiResponse = new ApiResponse<TResponse>(exception.Errors);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new ApiResponse<TResponse>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    public async Task PostAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? createValidator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            if (createValidator != null)
                await createValidator.ValidateAndThrowAsync(request);
            var response = await httpClient.PostAsJsonAsync($"{Base}{url}", request);
            apiResponse = await HandleResponse<object>(response);
        }
        catch (ValidationException exception)
        {
            apiResponse = new ApiResponse<object>(exception);
        }
        catch (ApiValidationException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Errors);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    public async Task PutAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? updateValidator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            if (updateValidator != null)
                await updateValidator.ValidateAndThrowAsync(request);
            var response = await httpClient.PutAsJsonAsync($"{Base}{url}", request);
            apiResponse = await HandleResponse<object>(response);
        }
        catch (ValidationException exception)
        {
            apiResponse = new ApiResponse<object>(exception);
        }
        catch (ApiValidationException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Errors);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    public async Task DeleteAsync(string url,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null)
    {
        IApiResponse<object> apiResponse;
        try
        {
            var response = await httpClient.DeleteAsync($"{Base}{url}");
            apiResponse = await HandleResponse<object>(response);
        }
        catch (ApiValidationException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Errors);
        }
        catch (HttpRequestException exception)
        {
            apiResponse = new ApiResponse<object>(exception.Message);
        }

        if (apiResponse.IsSuccess)
            if (onSuccess != null) await onSuccess(apiResponse);
        if (!apiResponse.IsSuccess)
            if (onFailure != null) await onFailure(apiResponse);
    }

    private static async Task<IApiResponse<TResponse>> HandleResponse<TResponse>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (typeof(TResponse) == typeof(object)) return new ApiResponse<TResponse>();
            var content = await response.Content.ReadAsStringAsync();
            return new ApiResponse<TResponse>(JsonSerializer.Deserialize<TResponse>(content, Options)!);
        }

        try
        {
            var content = await response.Content.ReadAsStringAsync();

            var document = JsonSerializer.Deserialize<JsonElement>(content, Options);

            if (response.StatusCode == HttpStatusCode.BadRequest &&
                document.TryGetProperty("errors", out var errorsElement))
            {
                var errors =
                    JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText(), Options)
                    ?? new Dictionary<string, string[]>();
                throw new ApiValidationException(errors);
            }

            if (document.TryGetProperty("detail", out var detail))
                return new ApiResponse<TResponse>(detail.GetString() ?? $"Unexpected status: {response.StatusCode}");
        }
        catch (JsonException)
        {
            // ignore
        }

        return new ApiResponse<TResponse>($"Unexpected status: {response.StatusCode}");
    }
}