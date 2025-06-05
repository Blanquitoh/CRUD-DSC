using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
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

        return await context.Countries
            .Where(c => c.CountryId == request.Id)
            .ProjectTo<CountryGetByIdResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}