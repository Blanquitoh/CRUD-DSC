using FluentValidation;
using Sakila.Contracts.Languages.Commands;

namespace Sakila.Contracts.Languages.Validators;

public class LanguageDeleteValidator : AbstractValidator<LanguageDeleteRequest>
{
    public LanguageDeleteValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
