using System.Collections.Generic;

namespace Sakila.Contracts.Countries.Queries.Responses;

public class CountryGetAllResponse
{
    public List<CountryGetByIdResponse> Countries { get; set; } = new();
}