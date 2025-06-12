using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Contracts.Languages.Commands;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Commands.Validators;

public class LanguageDeleteValidator : ValidatorWithData<LanguageDeleteRequest, LanguageDeleteValidatorData>
{
    public LanguageDeleteValidator(ISakilaContext dbContext)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var language = await dbContext.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                SetData(ctx, new LanguageDeleteValidatorData(language));
                return true;
            })
            .WithMessage("Language not found.");
    }
}