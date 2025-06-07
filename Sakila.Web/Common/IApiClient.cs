using FluentValidation;
using Sakila.Contracts.Common;

namespace Sakila.Web.Common;

public interface IApiClient
{
    Task<IApiResponse<TResponse>> GetAsync<TResponse>(string url);

    Task<IApiResponse<object>>
        PostAsync<TRequest>(string url, TRequest request, IValidator<TRequest>? validator = null);

    Task<IApiResponse<object>> PutAsync<TRequest>(string url, TRequest request, IValidator<TRequest>? validator = null);
    Task<IApiResponse<object>> DeleteAsync(string url);
}