using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<CountryUpdateRequest, Country> validator)
    : IRequestHandler<CountryUpdateRequest, Unit>
{
    public async Task<Unit> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        var country = await validator.ValidateAndGetDataAsync(request, cancellationToken);

        mapper.Map(request, country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
