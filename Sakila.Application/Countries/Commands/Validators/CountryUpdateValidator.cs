using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CountryUpdateValidator : ValidatorWithData<CountryUpdateRequest, CountryUpdateValidatorData>
{
    public CountryUpdateValidator(SakilaContext dbContext)
    {
        Include(new Contracts.Countries.Commands.Validators.CountryUpdateValidator());

        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await dbContext.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                SetData(ctx, new CountryUpdateValidatorData(country));
                return true;
            })
            .WithMessage("Country not found.");

        RuleFor(x => x.Name)
            .MustAsync(async (request, name, ctx, ct) =>
                !await dbContext.Countries.AnyAsync(c => c.Country1 == name && c.CountryId != request.Id, ct))
            .WithMessage("Another country with this name already exists.");
    }
}