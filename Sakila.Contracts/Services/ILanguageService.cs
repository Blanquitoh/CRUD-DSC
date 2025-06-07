using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries.Responses;
using System.Threading.Tasks;
using Sakila.Contracts.Common;

namespace Sakila.Contracts.Services;

public interface ILanguageService
{
    Task<IApiResponse<LanguageGetAllResponse>> GetAllAsync();
    Task<IApiResponse<LanguageGetByIdResponse>> GetByIdAsync(int id);
    Task<IApiResponse<object>> CreateAsync(LanguageCreateRequest request);
    Task<IApiResponse<object>> UpdateAsync(LanguageUpdateRequest request);
    Task<IApiResponse<object>> DeleteAsync(int id);
}
