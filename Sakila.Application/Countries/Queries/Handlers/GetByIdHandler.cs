using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Handlers;

public class GetByIdHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<CountryGetByIdRequest, Country> validator)
    : IRequestHandler<CountryGetByIdRequest, CountryGetByIdResponse?>
{
    public async Task<CountryGetByIdResponse?> Handle(CountryGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var country = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        return mapper.Map<CountryGetByIdResponse>(country);
    }
}
