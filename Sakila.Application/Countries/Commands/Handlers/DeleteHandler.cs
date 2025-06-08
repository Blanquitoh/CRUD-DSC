using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<CountryDeleteRequest, CountryDeleteValidatorData> validator)
    : IRequestHandler<CountryDeleteRequest, bool>
{
    public async Task<bool> Handle(CountryDeleteRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        dbContext.Countries.Remove(data.Country);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}