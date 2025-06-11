using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Api;

namespace Sakila.Web.Services.Implementations;

public class LanguageService : BaseCrudService<LanguageCreateRequest, LanguageUpdateRequest, LanguageGetAllResponse, LanguageGetByIdResponse>, ILanguageService
{
    private readonly ILanguagesApi api;

    public LanguageService(ILanguagesApi api,
        IValidator<LanguageCreateRequest> createValidator,
        IValidator<LanguageUpdateRequest> updateValidator)
        : base(createValidator, updateValidator)
    {
        this.api = api;
    }

    protected override int GetUpdateId(LanguageUpdateRequest request) => request.Id;

    protected override Task<LanguageGetAllResponse> GetAllApiAsync() => api.GetAllAsync();

    protected override Task<LanguageGetByIdResponse?> GetByIdApiAsync(int id) => api.GetByIdAsync(id);

    protected override Task CreateApiAsync(LanguageCreateRequest request) => api.PostAsync(request);

    protected override Task UpdateApiAsync(int id, LanguageUpdateRequest request) => api.PutAsync(id, request);

    protected override Task DeleteApiAsync(int id) => api.DeleteAsync(id);
}
