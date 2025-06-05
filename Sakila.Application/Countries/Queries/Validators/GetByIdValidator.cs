using FluentValidation;
using Sakila.Contracts.Countries.Queries;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Validators;

public class GetByIdValidator : AbstractValidator<CountryGetByIdRequest>
{
    public GetByIdValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .Must(id => context.Countries.Any(c => c.CountryId == id))
            .WithMessage("Country not found.");
    }
}