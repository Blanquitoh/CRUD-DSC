using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Queries.Validators.Data;

public class LanguageGetByIdValidatorData(Language language)
{
    public Language Language { get; set; } = language;
}
