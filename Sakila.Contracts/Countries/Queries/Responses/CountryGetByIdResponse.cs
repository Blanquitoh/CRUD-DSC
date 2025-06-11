using System.ComponentModel.DataAnnotations;

namespace Sakila.Contracts.Countries.Queries.Responses;

public class CountryGetByIdResponse
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Country name is required.")]
    [MaxLength(50, ErrorMessage = "Country name must be 50 characters or fewer.")]
    public string Name { get; set; } = string.Empty;
}