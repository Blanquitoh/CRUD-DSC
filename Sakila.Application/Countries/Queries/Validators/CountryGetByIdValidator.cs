using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Queries.Validators.Data;
using Sakila.Contracts.Countries.Queries;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Countries.Queries.Validators;

public class CountryGetByIdValidator : ValidatorWithData<CountryGetByIdRequest, CountryGetByIdValidatorData>
{
    public CountryGetByIdValidator(ISakilaContext dbContext)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                SetData(ctx, new CountryGetByIdValidatorData(country));
                return true;
            })
            .WithMessage("Country not found.");
    }
}