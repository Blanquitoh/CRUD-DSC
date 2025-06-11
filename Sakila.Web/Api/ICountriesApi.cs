using Refit;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Api;

public interface ICountriesApi
{
    [Get("/api/countries")]
    Task<CountryGetAllResponse> GetAllAsync();

    [Get("/api/countries/{id}")]
    Task<CountryGetByIdResponse?> GetByIdAsync(int id);

    [Post("/api/countries")]
    Task PostAsync([Body] CountryCreateRequest request);

    [Put("/api/countries/{id}")]
    Task PutAsync(int id, [Body] CountryUpdateRequest request);

    [Delete("/api/countries/{id}")]
    Task DeleteAsync(int id);
}