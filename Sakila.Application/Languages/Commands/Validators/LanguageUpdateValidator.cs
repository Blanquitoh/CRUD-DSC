using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class LanguageUpdateValidator : ValidatorWithData<LanguageUpdateRequest, Language>
{
    public LanguageUpdateValidator(SakilaContext dbContext)
    {
        Include(new Contracts.Languages.Commands.Validators.LanguageUpdateValidator());

        RuleFor(x => x.Id)
            .MustAsync(async (cmd, id, ctx, ct) =>
            {
                var language = await dbContext.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                SetData(ctx, language);
                return true;
            })
            .WithMessage("Language not found.");

        RuleFor(x => x.Name)
            .MustAsync(async (cmd, name, ctx, ct) =>
                !await dbContext.Languages.AnyAsync(l => l.Name == name && l.LanguageId != cmd.Id, ct))
            .WithMessage("Another language with this name already exists.");
    }
}