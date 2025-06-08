using FluentValidation;
using Sakila.Web.Abstractions;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Common;

namespace Sakila.Web.Services.Implementations;

public class LanguageService(
    IApiClient apiClient,
    IValidator<LanguageCreateRequest> createValidator,
    IValidator<LanguageUpdateRequest> updateValidator) : ILanguageService
{
    private readonly string _resource = "languages";

    public async Task<IApiResponse<LanguageGetAllResponse>> GetAllAsync()
    {
        return await apiClient.GetAsync<LanguageGetAllResponse>(_resource);
    }

    public async Task<IApiResponse<LanguageGetByIdResponse>> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<LanguageGetByIdResponse>($"{_resource}/{id}");
    }

    public async Task<IApiResponse<object>> CreateAsync(LanguageCreateRequest request)
    {
        return await apiClient.PostAsync(_resource, request, createValidator);
    }

    public async Task<IApiResponse<object>> UpdateAsync(LanguageUpdateRequest request)
    {
        return await apiClient.PutAsync($"{_resource}/{request.Id}", request, updateValidator);
    }

    public async Task<IApiResponse<object>> DeleteAsync(int id)
    {
        return await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}