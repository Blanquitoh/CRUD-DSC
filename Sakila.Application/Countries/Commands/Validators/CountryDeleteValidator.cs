using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CountryDeleteValidator : ValidatorWithData<CountryDeleteRequest, CountryDeleteValidatorData>
{
    public CountryDeleteValidator(SakilaContext dbContext)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                SetData(ctx, new CountryDeleteValidatorData(country));
                return true;
            })
            .WithMessage("Country not found.");
    }
}