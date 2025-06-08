namespace Sakila.Contracts.Languages.Queries.Responses;

public class LanguageGetByIdResponse
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
}