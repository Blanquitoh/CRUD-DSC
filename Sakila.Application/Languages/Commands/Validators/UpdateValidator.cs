using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Validators;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class UpdateValidator : AbstractValidator<LanguageUpdateRequest>
{
    public UpdateValidator(SakilaContext context)
    {
        Include(new LanguageUpdateValidator());

        RuleFor(x => x.Id)
            .MustAsync(async (cmd, id, ctx, ct) =>
            {
                var language = await context.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                ctx.RootContextData["language"] = language;
                return true;
            })
            .WithMessage("Language not found.");

        RuleFor(x => x.Name)
            .MustAsync(async (cmd, name, _, ct) =>
                !await context.Languages.AnyAsync(l => l.Name == name && l.LanguageId != cmd.Id, ct))
            .WithMessage("Another language with this name already exists.");
    }
}