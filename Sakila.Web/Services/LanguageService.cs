using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;
using Sakila.Contracts.Services;
using Sakila.Web.Common;

namespace Sakila.Web.Services;

public class LanguageService(
    IApiClient apiClient,
    IValidator<LanguageCreateRequest> createValidator,
    IValidator<LanguageUpdateRequest> updateValidator,
    IValidator<LanguageDeleteRequest> deleteValidator) : ILanguageService
{
    private readonly string _resource = "languages";
    private readonly IValidator<LanguageCreateRequest> _createValidator = createValidator;
    private readonly IValidator<LanguageUpdateRequest> _updateValidator = updateValidator;
    private readonly IValidator<LanguageDeleteRequest> _deleteValidator = deleteValidator;

    public async Task<LanguageGetAllResponse> GetAllAsync()
    {
        return await apiClient.GetAsync<LanguageGetAllResponse>(_resource);
    }

    public async Task<LanguageGetByIdResponse> GetByIdAsync(int id)
    {
        return await apiClient.GetAsync<LanguageGetByIdResponse>($"{_resource}/{id}");
    }

    public async Task CreateAsync(LanguageCreateRequest request)
    {
        await _createValidator.ValidateAndThrowAsync(request);
        await apiClient.PostAsync(_resource, request);
    }

    public async Task UpdateAsync(LanguageUpdateRequest request)
    {
        await _updateValidator.ValidateAndThrowAsync(request);
        await apiClient.PutAsync($"{_resource}/{request.Id}", request);
    }

    public async Task DeleteAsync(int id)
    {
        var request = new LanguageDeleteRequest { Id = id };
        await _deleteValidator.ValidateAndThrowAsync(request);
        await apiClient.DeleteAsync($"{_resource}/{id}");
    }
}