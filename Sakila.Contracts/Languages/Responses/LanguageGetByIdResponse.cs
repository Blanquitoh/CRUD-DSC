namespace Sakila.Contracts.Languages.Responses;

public class LanguageGetByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}