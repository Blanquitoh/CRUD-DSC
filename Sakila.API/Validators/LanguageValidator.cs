using FluentValidation;
using Sakila.API.Data;
using Sakila.API.Models;

namespace Sakila.API.Validators;

public class LanguageValidator : AbstractValidator<Language>
{
    public LanguageValidator(SakilaContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(20).WithMessage("Language name must be 20 characters or fewer.");

        RuleSet("Create", () =>
        {
            RuleFor(x => x.Name)
                .Must(name => !context.Languages.Any(l => l.Name == name))
                .WithMessage("A language with this name already exists.");
        });

        RuleSet("Update", () =>
        {
            RuleFor(x => x.Name)
                .Must((language, name) =>
                    !context.Languages.Any(l => l.Name == name && l.LanguageId != language.LanguageId))
                .WithMessage("Another language with this name already exists.");
        });
    }
}