using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Web.Abstractions;

public interface ILanguageService : ICrudService<LanguageCreateRequest, LanguageUpdateRequest, LanguageGetAllResponse, LanguageGetByIdResponse>
{
}
