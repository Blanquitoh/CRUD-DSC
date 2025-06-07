using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Queries;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Validators;

public class CountryGetByIdValidator : AbstractValidator<CountryGetByIdRequest>
{
    public CountryGetByIdValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                ctx.RootContextData["country"] = country;
                return true;
            })
            .WithMessage("Country not found.");
    }
}