using FluentValidation;

namespace Sakila.Contracts.Countries.Commands.Validators;

public class CountryUpdateValidator : AbstractValidator<CountryUpdateRequest>
{
    public CountryUpdateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(50).WithMessage("Country name must be 50 characters or fewer.");
    }
}
