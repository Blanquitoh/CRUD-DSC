using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Handlers
{
    public class GetByIdHandler(SakilaContext context, IMapper mapper)
        : IRequestHandler<LanguageGetByIdRequest, LanguageGetByIdResponse?>
    {
        public async Task<LanguageGetByIdResponse?> Handle(LanguageGetByIdRequest request,
            CancellationToken cancellationToken)
        {
            return await context.Languages
                .Where(l => l.LanguageId == request.Id)
                .ProjectTo<LanguageGetByIdResponse>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}