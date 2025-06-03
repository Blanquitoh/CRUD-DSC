namespace Sakila.Contracts.Countries.Responses;

public class CountryGetAllResponse
{
    public List<CountryGetByIdResponse> Countries { get; set; } = new();
}
