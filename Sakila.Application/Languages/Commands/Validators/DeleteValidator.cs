using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Validators;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class DeleteValidator : AbstractValidator<LanguageDeleteRequest>
{
    public DeleteValidator(SakilaContext context)
    {
        Include(new LanguageDeleteValidator());

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