using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Validators;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class DeleteValidator : AbstractValidator<CountryDeleteRequest>
{
    public DeleteValidator(SakilaContext context)
    {
        Include(new CountryDeleteValidator());

        RuleFor(x => x.Id)
            .Must(id => context.Countries.Any(c => c.CountryId == id))
            .WithMessage("Country not found.");
    }
}