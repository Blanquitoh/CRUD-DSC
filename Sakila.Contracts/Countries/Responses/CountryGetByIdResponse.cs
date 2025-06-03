namespace Sakila.Contracts.Countries.Responses;

public class CountryGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
