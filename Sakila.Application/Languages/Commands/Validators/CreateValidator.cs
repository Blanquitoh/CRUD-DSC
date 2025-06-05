using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Validators;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class CreateValidator : AbstractValidator<LanguageCreateRequest>
{
    public CreateValidator(SakilaContext context)
    {
        Include(new LanguageCreateValidator());

        RuleFor(x => x.Name)
            .Must(name => !context.Languages.Any(l => l.Name == name))
            .WithMessage("A language with this name already exists.");
    }
}