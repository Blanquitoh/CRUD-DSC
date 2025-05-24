using FluentValidation;
using Sakila.Contracts.Languages.Queries;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Validators;

public class GetByIdValidator : AbstractValidator<LanguageGetByIdRequest>
{
    public GetByIdValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .Must(id => context.Languages.Any(l => l.LanguageId == id))
            .WithMessage("Language not found.");
    }
}