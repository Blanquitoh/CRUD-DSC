using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetAllHandler(ISakilaContext dbContext, IMapper mapper)
    : IRequestHandler<LanguageGetAllRequest, LanguageGetAllResponse>
{
    public async Task<LanguageGetAllResponse> Handle(LanguageGetAllRequest request,
        CancellationToken cancellationToken)
    {
        return new LanguageGetAllResponse
        {
            Languages = await mapper.ProjectTo<LanguageGetByIdResponse>(dbContext.Languages.AsNoTracking())
                .ToListAsync(cancellationToken)
        };
    }
}