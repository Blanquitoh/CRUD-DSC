using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<CountryUpdateRequest> validator)
    : IRequestHandler<CountryUpdateRequest, Unit>
{
    public async Task<Unit> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var country = await context.Countries
            .FirstAsync(c => c.CountryId == request.Id, cancellationToken);

        mapper.Map(request, country);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}