using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class CreateValidator : AbstractValidator<LanguageCreateRequest>
{
    public CreateValidator(SakilaContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(20).WithMessage("Language name must be 20 characters or fewer.")
            .Must(name => !context.Languages.Any(l => l.Name == name))
            .WithMessage("A language with this name already exists.");
    }
}