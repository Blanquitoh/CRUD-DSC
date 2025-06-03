using System.Threading.Tasks;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Responses;

namespace Sakila.Contracts.Services;

public interface ICountryService
{
    Task<CountryGetAllResponse> GetAllAsync();
    Task<CountryGetByIdResponse> GetByIdAsync(int id);
    Task CreateAsync(CountryCreateRequest request);
    Task UpdateAsync(CountryUpdateRequest request);
    Task DeleteAsync(int id);
}
