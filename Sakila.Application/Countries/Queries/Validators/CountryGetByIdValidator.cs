using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Queries;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Validators;

public class CountryGetByIdValidator : ValidatorFork<CountryGetByIdRequest, Country>
{
    public CountryGetByIdValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                SetData(ctx, country);
                return true;
            })
            .WithMessage("Country not found.");
    }
}
