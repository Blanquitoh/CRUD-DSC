using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<CountryDeleteRequest, Country> validator) : IRequestHandler<CountryDeleteRequest, bool>
{
    public async Task<bool> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        var country = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        dbContext.Countries.Remove(country);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}