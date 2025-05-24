using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class DeleteValidator : AbstractValidator<LanguageDeleteRequest>
{
    public DeleteValidator(SakilaContext context)
    {
        RuleFor(x => x.Id)
            .Must(id => context.Languages.Any(l => l.LanguageId == id))
            .WithMessage("Language not found.");
    }
}