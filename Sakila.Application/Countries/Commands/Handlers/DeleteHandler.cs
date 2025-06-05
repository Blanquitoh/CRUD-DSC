using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(
    SakilaContext context,
    IValidator<CountryDeleteRequest> validator) : IRequestHandler<CountryDeleteRequest, bool>
{
    public async Task<bool> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var country = await context.Countries.FirstAsync(c => c.CountryId == request.Id, cancellationToken);
        context.Countries.Remove(country);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}