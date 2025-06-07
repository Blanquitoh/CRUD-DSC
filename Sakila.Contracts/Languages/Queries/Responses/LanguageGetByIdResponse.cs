namespace Sakila.Contracts.Languages.Queries.Responses;

public class LanguageGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
