using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Validators;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CreateValidator : AbstractValidator<CountryCreateRequest>
{
    public CreateValidator(SakilaContext context)
    {
        Include(new CountryCreateValidator());

        RuleFor(x => x.Name)
            .Must(name => !context.Countries.Any(c => c.Country1 == name))
            .WithMessage("A country with this name already exists.");
    }
}