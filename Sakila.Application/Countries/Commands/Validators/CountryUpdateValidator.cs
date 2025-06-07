using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CountryUpdateValidator : AbstractValidator<CountryUpdateRequest>
{
    public CountryUpdateValidator(SakilaContext context)
    {
        Include(new Contracts.Countries.Commands.Validators.CountryUpdateValidator());

        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                ctx.RootContextData["country"] = country;
                return true;
            })
            .WithMessage("Country not found.");

        RuleFor(x => x.Name)
            .MustAsync(async (request, name, _, ct) =>
                !await context.Countries.AnyAsync(c => c.Country1 == name && c.CountryId != request.Id, ct))
            .WithMessage("Another country with this name already exists.");
    }
}