using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Commands.Validators.Data;

public class LanguageDeleteValidatorData(Language language)
{
    public Language Language { get; set; } = language;
}