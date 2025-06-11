using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageCreateRequest : IRequest<int>
{
    [Required(ErrorMessage = "Language name is required.")]
    [MaxLength(20, ErrorMessage = "Language name must be 20 characters or fewer.")]
    public string Name { get; init; } = string.Empty;
}