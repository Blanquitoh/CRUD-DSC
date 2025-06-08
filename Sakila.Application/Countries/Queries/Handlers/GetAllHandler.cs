using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Handlers;

public class GetAllHandler(SakilaContext dbContext, IMapper mapper)
    : IRequestHandler<CountryGetAllRequest, CountryGetAllResponse>
{
    public async Task<CountryGetAllResponse> Handle(CountryGetAllRequest request,
        CancellationToken cancellationToken)
    {
        return new CountryGetAllResponse
        {
            Countries = await mapper.ProjectTo<CountryGetByIdResponse>(dbContext.Countries)
                .ToListAsync(cancellationToken)
        };
    }
}
