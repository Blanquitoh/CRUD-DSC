using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Services.Implementations;

public class LanguageService(
    IApiClient apiClient,
    IValidator<LanguageCreateRequest> createValidator,
    IValidator<LanguageUpdateRequest> updateValidator) : ILanguageService
{
    private const string Resource = "languages";

    public async Task<IApiResponse<LanguageGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<LanguageGetAllResponse>(Resource);
    }

    public async Task<IApiResponse<LanguageGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<LanguageGetByIdResponse>($"{Resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(LanguageCreateRequest request)
    {
        return await apiClient.PostAsync(Resource, request, createValidator);
    }

    public async Task<IApiResponse<object>> UpdateAsync(LanguageUpdateRequest request)
    {
        return await apiClient.PutAsync($"{Resource}/{request.Id}", request, updateValidator);
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id)
    {
        return await apiClient.DeleteAsync($"{Resource}/{id}");
    }
}