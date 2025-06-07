using FluentValidation;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Sakila.Contracts.Common;

namespace Sakila.Web.Common;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private const string Base = "api/";

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    public async Task<IApiResponse<TResponse>> GetAsync<TResponse>(string url)
    {
        try
        {
            var response = await httpClient.GetAsync($"{Base}{url}");
            return await HandleResponse<TResponse>(response);
        }
        catch (ApiValidationException exception)
        {
            return new ApiResponse<TResponse>(exception.Errors);
        }
    }

    public async Task<IApiResponse<object>> PostAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? createValidator = null)
    {
        try
        {
            if (createValidator != null)
                await createValidator.ValidateAndThrowAsync(request);
            var response = await httpClient.PostAsJsonAsync($"{Base}{url}", request);
            return await HandleResponse<object>(response);
        }
        catch (ValidationException exception)
        {
            return new ApiResponse<object>(exception);
        }
        catch (ApiValidationException exception)
        {
            return new ApiResponse<object>(exception.Errors);
        }
    }

    public async Task<IApiResponse<object>> PutAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? updateValidator = null)
    {
        try
        {
            if (updateValidator != null)
                await updateValidator.ValidateAndThrowAsync(request);
            var response = await httpClient.PutAsJsonAsync($"{Base}{url}", request);
            return await HandleResponse<object>(response);
        }
        catch (ValidationException exception)
        {
            return new ApiResponse<object>(exception);
        }
        catch (ApiValidationException exception)
        {
            return new ApiResponse<object>(exception.Errors);
        }
    }

    public async Task<IApiResponse<object>> DeleteAsync(string url)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"{Base}{url}");
            return await HandleResponse<object>(response);
        }
        catch (ApiValidationException exception)
        {
            return new ApiResponse<object>(exception.Errors);
        }
    }

    private static async Task<IApiResponse<TResponse>> HandleResponse<TResponse>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (typeof(TResponse) == typeof(object)) return new ApiResponse<TResponse>();
            var content = await response.Content.ReadAsStringAsync();
            return new ApiResponse<TResponse>(JsonSerializer.Deserialize<TResponse>(content, Options)!);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var content = await response.Content.ReadAsStringAsync();

            // Try to parse validation error structure
            var problem = JsonSerializer.Deserialize<ValidationErrorResponse>(content, Options);

            if (problem?.Errors is { Count: > 0 })
                throw new ApiValidationException(problem.Errors);
        }

        throw new HttpRequestException(
            $"Unexpected status: {response.StatusCode}\n\n{await response.Content.ReadAsStringAsync()}");
    }
}