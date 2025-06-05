using FluentValidation;
using Sakila.Contracts.Countries.Commands;

namespace Sakila.Contracts.Countries.Validators;

public class CountryDeleteValidator : AbstractValidator<CountryDeleteRequest>
{
    public CountryDeleteValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
