using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class LanguageDeleteValidator : AbstractValidator<LanguageDeleteRequest>
{
    public LanguageDeleteValidator(SakilaContext context)
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
