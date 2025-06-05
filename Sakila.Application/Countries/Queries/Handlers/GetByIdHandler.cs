using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Domain.Models;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Handlers;

public class GetByIdHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<CountryGetByIdRequest> validator)
    : IRequestHandler<CountryGetByIdRequest, CountryGetByIdResponse?>
{
    public async Task<CountryGetByIdResponse?> Handle(CountryGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var ctx = new ValidationContext<CountryGetByIdRequest>(request);
        var country = (Country)ctx.RootContextData["country"];
        return mapper.Map<CountryGetByIdResponse>(country);
    }
}