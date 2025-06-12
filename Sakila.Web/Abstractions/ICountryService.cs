using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Web.Abstractions;

public interface ICountryService : ICrudService<CountryCreateRequest, CountryUpdateRequest, CountryGetAllResponse, CountryGetByIdResponse>
{
}
