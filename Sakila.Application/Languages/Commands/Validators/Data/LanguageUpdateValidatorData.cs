using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Commands.Validators.Data;

public class LanguageUpdateValidatorData(Language language)
{
    public Language Language { get; set; } = language;
}
