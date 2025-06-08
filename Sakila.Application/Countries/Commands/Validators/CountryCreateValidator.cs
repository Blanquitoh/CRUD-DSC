using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CountryCreateValidator : AbstractValidator<CountryCreateRequest>
{
    public CountryCreateValidator(SakilaContext dbContext)
    {
        Include(new Contracts.Countries.Commands.Validators.CountryCreateValidator());

        RuleFor(x => x.Name)
            .Must(name => !dbContext.Countries.Any(c => c.Country1 == name))
            .WithMessage("A country with this name already exists.");
    }
}
