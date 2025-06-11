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

    public async Task<IApiResponse<CountryGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<CountryGetAllResponse>(Resource);
    }

    public async Task<IApiResponse<CountryGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<CountryGetByIdResponse>($"{Resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(CountryCreateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.PostAsync(Resource, request, createValidator);

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            EditContext.ApplyErrors(MessageStore, response);
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }

    public async Task<IApiResponse<object>> UpdateAsync(CountryUpdateRequest request,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator);

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            EditContext.ApplyErrors(MessageStore, response);
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id,
        Func<IApiResponse<object>, Task>? onSuccess = null,
        Func<Dictionary<string, string[]>, Task>? onFailure = null)
    {
        var response = await apiClient.DeleteAsync($"{Resource}/{id}");

        if (response.IsSuccess)
        {
            if (onSuccess != null) await onSuccess(response);
        }
        else
        {
            if (onFailure != null) await onFailure(response.Errors);
        }

        return response;
    }
}