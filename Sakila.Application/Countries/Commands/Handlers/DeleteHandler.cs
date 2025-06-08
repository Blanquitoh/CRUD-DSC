using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(
    SakilaContext context,
    IValidatorFork<CountryDeleteRequest, Country> validator) : IRequestHandler<CountryDeleteRequest, bool>
{
    public async Task<bool> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<CountryDeleteRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var country = validator.GetData(validationContext)!;
        context.Countries.Remove(country);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
