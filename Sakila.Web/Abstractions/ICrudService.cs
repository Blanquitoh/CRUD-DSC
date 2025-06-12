using Microsoft.AspNetCore.Components.Forms;

namespace Sakila.Web.Abstractions;

public interface ICrudService<TCreate, TUpdate, TGetAll, TGetById>
{
    EditContext EditContext { get; }
    ValidationMessageStore MessageStore { get; }
    void Initialize(object model);

    Task GetAllAsync(
        Func<ISakilaApiResponse<TGetAll>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<TGetAll>, Task>? onFailure = null);

    Task GetByIdAsync(int id,
        Func<ISakilaApiResponse<TGetById>, Task>? onSuccess = null,
        Func<ISakilaApiResponse<TGetById>, Task>? onFailure = null);

    Task CreateAsync(TCreate request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task UpdateAsync(TUpdate request,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);

    Task DeleteAsync(int id,
        Func<ISakilaApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null);
}
