using Refit;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Web.Api;

public interface ILanguagesApi
{
    [Get("/api/languages")]
    Task<LanguageGetAllResponse> GetAllAsync();

    [Get("/api/languages/{id}")]
    Task<LanguageGetByIdResponse?> GetByIdAsync(int id);

    [Post("/api/languages")]
    Task PostAsync([Body] LanguageCreateRequest request);

    [Put("/api/languages/{id}")]
    Task PutAsync(int id, [Body] LanguageUpdateRequest request);

    [Delete("/api/languages/{id}")]
    Task DeleteAsync(int id);
}
