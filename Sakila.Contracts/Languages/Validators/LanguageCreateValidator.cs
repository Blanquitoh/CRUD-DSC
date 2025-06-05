using FluentValidation;
using Sakila.Contracts.Languages.Commands;

namespace Sakila.Contracts.Languages.Validators;

public class LanguageCreateValidator : AbstractValidator<LanguageCreateRequest>
{
    public LanguageCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(20).WithMessage("Language name must be 20 characters or fewer.");
    }
}
