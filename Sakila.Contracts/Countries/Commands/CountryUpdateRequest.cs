using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Sakila.Contracts.Countries.Commands;

public class CountryUpdateRequest : IRequest<Unit>
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Country name is required.")]
    [MaxLength(50, ErrorMessage = "Country name must be 50 characters or fewer.")]
    public string Name { get; set; } = string.Empty;
}