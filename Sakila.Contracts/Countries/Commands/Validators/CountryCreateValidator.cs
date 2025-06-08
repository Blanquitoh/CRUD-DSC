using FluentValidation;

namespace Sakila.Contracts.Countries.Commands.Validators;

public class CountryCreateValidator : AbstractValidator<CountryCreateRequest>
{
    public CountryCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(50).WithMessage("Country name must be 50 characters or fewer.");
    }
}