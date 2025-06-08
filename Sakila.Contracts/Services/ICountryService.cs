using System.Threading.Tasks;
using Sakila.Contracts.Common;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Contracts.Services;

public interface ICountryService
{
    Task<IApiResponse<CountryGetAllResponse>> GetAllAsync();
    Task<IApiResponse<CountryGetByIdResponse>> GetByIdAsync(int id);
    Task<IApiResponse<object>> CreateAsync(CountryCreateRequest request);
    Task<IApiResponse<object>> UpdateAsync(CountryUpdateRequest request);
    Task<IApiResponse<object>> DeleteAsync(int id);
}
