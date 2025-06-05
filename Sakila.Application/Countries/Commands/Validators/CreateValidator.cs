using FluentValidation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Validators;

public class CreateValidator : AbstractValidator<CountryCreateRequest>
{
    public CreateValidator(SakilaContext context)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Country name is required.")
            .MaximumLength(50).WithMessage("Country name must be 50 characters or fewer.")
            .Must(name => !context.Countries.Any(c => c.Country1 == name))
            .WithMessage("A country with this name already exists.");
    }
}