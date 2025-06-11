using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageUpdateRequest : IRequest<Unit>
{
    public int Id { get; init; }

    [Required(ErrorMessage = "Language name is required.")]
    [MaxLength(20, ErrorMessage = "Language name must be 20 characters or fewer.")]
    public string Name { get; set; } = string.Empty;
}