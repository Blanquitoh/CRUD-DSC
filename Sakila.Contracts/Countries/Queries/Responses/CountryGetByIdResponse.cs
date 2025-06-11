namespace Sakila.Contracts.Countries.Queries.Responses;

public class CountryGetByIdResponse
{
    public int Id { get; init; }

    public string Name { get; set; } = string.Empty;
}