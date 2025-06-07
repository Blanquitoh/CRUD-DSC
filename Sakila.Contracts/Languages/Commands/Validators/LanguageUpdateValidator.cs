using FluentValidation;

namespace Sakila.Contracts.Languages.Commands.Validators;

public class LanguageUpdateValidator : AbstractValidator<LanguageUpdateRequest>
{
    public LanguageUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(20).WithMessage("Language name must be 20 characters or fewer.");
    }
}