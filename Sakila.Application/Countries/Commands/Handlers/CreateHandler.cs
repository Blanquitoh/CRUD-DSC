using AutoMapper;
using FluentValidation;
using Sakila.Application.Common.Handlers;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class CreateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidator<CountryCreateRequest> validator) :
    CreateHandlerBase<CountryCreateRequest, Country, int>(dbContext, mapper, validator)
{
    protected override int GetResponse(Country entity)
    {
        return entity.CountryId;
    }
}