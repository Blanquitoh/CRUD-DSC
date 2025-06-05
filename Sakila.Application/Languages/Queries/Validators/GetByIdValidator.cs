using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Queries;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Validators;

public class GetByIdValidator : AbstractValidator<LanguageGetByIdRequest>
{
    public GetByIdValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var language = await context.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                ctx.RootContextData["language"] = language;
                return true;
            })
            .WithMessage("Language not found.");
    }
}