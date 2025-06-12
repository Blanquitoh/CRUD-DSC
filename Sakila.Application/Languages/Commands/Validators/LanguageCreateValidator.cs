using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Commands.Validators;

public class LanguageCreateValidator : AbstractValidator<LanguageCreateRequest>
{
    public LanguageCreateValidator(ISakilaContext dbContext)
    {
        Include(new Contracts.Languages.Commands.Validators.LanguageCreateValidator());

        RuleFor(x => x.Name)
            .Must(name => !dbContext.Languages.Any(l => l.Name == name))
            .WithMessage("A language with this name already exists.");
    }
}