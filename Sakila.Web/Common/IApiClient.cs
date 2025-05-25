namespace Sakila.Web.Common;

public interface IApiClient
{
    Task<TResponse> GetAsync<TResponse>(string url);
    Task PostAsync<TRequest>(string url, TRequest request);
    Task PutAsync<TRequest>(string url, TRequest request);
    Task DeleteAsync(string url);
}