using FluentValidation;

namespace Sakila.Web.Abstractions;

public interface IApiClient
{
    Task GetAsync<TResponse>(string url,
        Func<IApiResponse<TResponse>, Task>? onSuccess = null,
        Func<IApiResponse<TResponse>, Task>? onFailure = null);

    Task PostAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? validator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);

    Task PutAsync<TRequest>(string url, TRequest request,
        IValidator<TRequest>? validator = null,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);

    Task DeleteAsync(string url,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<IApiResponse<object>, Task>? onFailure = null);
}