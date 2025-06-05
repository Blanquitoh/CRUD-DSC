using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Validators;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class UpdateValidator : AbstractValidator<CountryUpdateRequest>
{
    public UpdateValidator(SakilaContext context)
    {
        Include(new CountryUpdateValidator());

        RuleFor(x => x.Id)
            .MustAsync(async (cmd, id, ctx, ct) =>
            {
                var country = await context.Countries.FirstOrDefaultAsync(c => c.CountryId == id, ct);
                if (country == null) return false;
                ctx.RootContextData["country"] = country;
                return true;
            })
            .WithMessage("Country not found.");

        RuleFor(x => x.Name)
            .MustAsync(async (cmd, name, _, ct) =>
                !await context.Countries.AnyAsync(c => c.Country1 == name && c.CountryId != cmd.Id, ct))
            .WithMessage("Another country with this name already exists.");
    }
}