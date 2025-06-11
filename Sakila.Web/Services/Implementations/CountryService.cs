using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Web.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using Sakila.Web.Extensions;

namespace Sakila.Web.Services.Implementations;

public class CountryService(
    IApiClient apiClient,
    IValidator<CountryCreateRequest> createValidator,
    IValidator<CountryUpdateRequest> updateValidator) : ICountryService
{
    private const string Resource = "countries";

    public EditContext EditContext { get; private set; } = null!;
    public ValidationMessageStore MessageStore { get; private set; } = null!;

    public void Initialize(object model)
    {
        EditContext = new EditContext(model);
        MessageStore = new ValidationMessageStore(EditContext);
    }

    public async Task GetAllAsync(
        Func<IApiResponse<CountryGetAllResponse>, Task>? onSuccess = null,
        Func<IApiResponse<CountryGetAllResponse>, Task>? onFailure = null)
    {
        await apiClient.GetAsync(Resource, onSuccess, onFailure);
    }

    public async Task GetByIdAsync(int id,
        Func<IApiResponse<CountryGetByIdResponse>, Task>? onSuccess = null,
        Func<IApiResponse<CountryGetByIdResponse>, Task>? onFailure = null)
    {
        await apiClient.GetAsync($"{Resource}/{id}", onSuccess, onFailure);
    }

    public async Task CreateAsync(CountryCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PostAsync(Resource, request, createValidator,
            onSuccess,
            async response =>
            {
                EditContext.ApplyErrors(MessageStore, response);
                if (onFailure != null) await onFailure(response.Errors);
            });
    }

    public async Task UpdateAsync(CountryUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator,
            onSuccess,
            async response =>
            {
                EditContext.ApplyErrors(MessageStore, response);
                if (onFailure != null) await onFailure(response.Errors);
            });
    }

    public async Task DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        await apiClient.DeleteAsync($"{Resource}/{id}",
            onSuccess,
            async response =>
            {
                if (onFailure != null) await onFailure(response.Errors);
            });
    }
}