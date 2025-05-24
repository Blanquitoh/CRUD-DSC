using System.Threading.Tasks;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Responses;

namespace Sakila.Contracts.Services;

public interface ILanguageService
{
    Task<LanguageGetAllResponse> GetAllAsync();
    Task<LanguageGetByIdResponse> GetByIdAsync(int id);
    Task<int> CreateAsync(LanguageCreateRequest request);
    Task UpdateAsync(LanguageUpdateRequest request);
    Task DeleteAsync(int id);
}