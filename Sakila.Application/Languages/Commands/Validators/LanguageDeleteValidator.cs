using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class LanguageDeleteValidator : ValidatorFork<LanguageDeleteRequest, Language>
{
    public LanguageDeleteValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var language = await context.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                SetData(ctx, language);
                return true;
            })
            .WithMessage("Language not found.");
    }
}
