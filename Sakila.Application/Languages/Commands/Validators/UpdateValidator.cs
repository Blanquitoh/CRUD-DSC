using FluentValidation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Validators;

public class UpdateValidator : AbstractValidator<LanguageUpdateRequest>
{
    public UpdateValidator(SakilaContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Language name is required.")
            .MaximumLength(20).WithMessage("Language name must be 20 characters or fewer.")
            .Must((cmd, name) =>
                !context.Languages.Any(l => l.Name == name && l.LanguageId != cmd.Id))
            .WithMessage("Another language with this name already exists.");
    }
}