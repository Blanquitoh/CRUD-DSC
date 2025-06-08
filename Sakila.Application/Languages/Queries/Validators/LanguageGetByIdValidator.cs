using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Queries;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Validators;

public class LanguageGetByIdValidator : ValidatorFork<LanguageGetByIdRequest, Language>
{
    public LanguageGetByIdValidator(SakilaContext context)
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
