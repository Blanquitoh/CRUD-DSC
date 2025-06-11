using System.ComponentModel.DataAnnotations;

namespace Sakila.Contracts.Languages.Queries.Responses;

public class LanguageGetByIdResponse
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Language name is required.")]
    [MaxLength(20, ErrorMessage = "Language name must be 20 characters or fewer.")]
    public string Name { get; set; } = string.Empty;
}