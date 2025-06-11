using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Web.Abstractions;
using Sakila.Web.Services;

namespace Sakila.Web.Services.Implementations;

public class LanguageService(
    IApiClient apiClient,
    IValidator<LanguageCreateRequest> createValidator,
    IValidator<LanguageUpdateRequest> updateValidator)
    : BaseCrudService<LanguageCreateRequest, LanguageUpdateRequest, LanguageGetAllResponse, LanguageGetByIdResponse>(
        "languages", apiClient, createValidator, updateValidator), ILanguageService
{
    protected override int GetUpdateId(LanguageUpdateRequest request) => request.Id;
}
