using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sakila.Web.Common;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    private const string Base = "api/";

    public async Task<TResponse> GetAsync<TResponse>(string url)
    {
        var response = await httpClient.GetAsync($"{Base}{url}");
        return await HandleResponse<TResponse>(response);
    }

    public async Task PostAsync<TRequest>(string url, TRequest request)
    {
        var response = await httpClient.PostAsJsonAsync($"{Base}{url}", request);
        await HandleResponse<object>(response);
    }

    public async Task PutAsync<TRequest>(string url, TRequest request)
    {
        var response = await httpClient.PutAsJsonAsync($"{Base}{url}", request);
        await HandleResponse<object>(response);
    }

    public async Task DeleteAsync(string url)
    {
        var response = await httpClient.DeleteAsync($"{Base}{url}");
        await HandleResponse<object>(response);
    }

    private static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            if (typeof(T) == typeof(object)) return default!;
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, Options)!;
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

public class ValidationErrorResponse
{
    public Dictionary<string, string[]> Errors { get; set; } = new();
}

public class ApiValidationException(Dictionary<string, string[]> errors) : Exception("Validation failed")
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}