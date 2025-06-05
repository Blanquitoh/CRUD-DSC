using FluentValidation;
using Sakila.Contracts.Countries.Commands;

namespace Sakila.Contracts.Countries.Validators;

public class CountryCreateValidator : AbstractValidator<CountryCreateRequest>
{
    public CountryCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(50).WithMessage("Country name must be 50 characters or fewer.");
    }
}
