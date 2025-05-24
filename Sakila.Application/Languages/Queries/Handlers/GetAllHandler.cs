using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetAllHandler(SakilaContext context, IMapper mapper)
    : IRequestHandler<LanguageGetAllRequest, LanguageGetAllResponse>
{
    public async Task<LanguageGetAllResponse> Handle(LanguageGetAllRequest request,
        CancellationToken cancellationToken)
    {
        return new LanguageGetAllResponse
        {
            Languages = await mapper.ProjectTo<LanguageGetByIdResponse>(context.Languages)
                .ToListAsync(cancellationToken)
        };
    }
}