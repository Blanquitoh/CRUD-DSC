using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(SakilaContext context) : IRequestHandler<CountryDeleteRequest, bool>
{
    public async Task<bool> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        var country = await context.Countries.FirstAsync(c => c.CountryId == request.Id, cancellationToken);
        context.Countries.Remove(country);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}