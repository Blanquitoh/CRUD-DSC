using FluentValidation;
using Sakila.Application.Common.Validation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CountryDeleteValidator : ValidatorFork<CountryDeleteRequest, Country>
{
    public CountryDeleteValidator(SakilaContext context)
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
