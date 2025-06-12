using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Queries.Validators.Data;
using Sakila.Contracts.Languages.Queries;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Queries.Validators;

public class LanguageGetByIdValidator : ValidatorWithData<LanguageGetByIdRequest, LanguageGetByIdValidatorData>
{
    public LanguageGetByIdValidator(ISakilaContext dbContext)
    {
        RuleFor(x => x.Id)
            .MustAsync(async (_, id, ctx, ct) =>
            {
                var language = await dbContext.Languages.FirstOrDefaultAsync(l => l.LanguageId == id, ct);
                if (language == null) return false;
                SetData(ctx, new LanguageGetByIdValidatorData(language));
                return true;
            })
            .WithMessage("Language not found.");
    }
}