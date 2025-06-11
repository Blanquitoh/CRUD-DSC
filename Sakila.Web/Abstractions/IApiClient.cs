using FluentValidation;

namespace Sakila.Web.Abstractions;

public interface IApiClient
{
    Task<IApiResponse<TResponse>> GetAsync<TResponse>(string url,
        Func<IApiResponse<TResponse>, Task>? onSuccess = null,
        Func<IApiResponse<TResponse>, Task>? onFailure = null);

    Task<IApiResponse<object>> PostAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? validator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);

    Task<IApiResponse<object>> PutAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? validator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);

    Task<IApiResponse<object>> DeleteAsync(string url,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);
}