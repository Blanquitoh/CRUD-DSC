using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<CountryDeleteRequest, CountryDeleteValidatorData> validator)
    : DeleteHandlerBase<CountryDeleteRequest, Country, CountryDeleteValidatorData>(dbContext, validator)
{
    protected override Country GetData(CountryDeleteValidatorData data)
    {
        return data.Country;
    }
}