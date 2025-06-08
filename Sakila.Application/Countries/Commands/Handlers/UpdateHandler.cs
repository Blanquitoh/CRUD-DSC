using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<CountryUpdateRequest, CountryUpdateValidatorData> validator)
    : IRequestHandler<CountryUpdateRequest, Unit>
{
    public async Task<Unit> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);

        mapper.Map(request, data.Country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}