using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class DeleteValidator : AbstractValidator<CountryDeleteRequest>
{
    public DeleteValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .Must(id => context.Countries.Any(c => c.CountryId == id))
            .WithMessage("Country not found.");
    }
}
