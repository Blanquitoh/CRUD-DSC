namespace Sakila.Contracts.Countries.Queries.Responses;

public class CountryGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}