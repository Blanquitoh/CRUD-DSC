using AutoMapper;
using MediatR;
using Sakila.Application.Common.Handlers;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Commands.Validators.Data;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<CountryUpdateRequest, CountryUpdateValidatorData> validator)
    : UpdateHandlerBase<CountryUpdateRequest, Country, CountryUpdateValidatorData>(dbContext, mapper, validator)
{
    protected override Country GetData(CountryUpdateValidatorData data) => data.Country;
}