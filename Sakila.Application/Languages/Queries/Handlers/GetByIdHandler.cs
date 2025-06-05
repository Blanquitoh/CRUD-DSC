using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetByIdHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<LanguageGetByIdRequest> validator)
    : IRequestHandler<LanguageGetByIdRequest, LanguageGetByIdResponse?>
{
    public async Task<LanguageGetByIdResponse?> Handle(LanguageGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        return await context.Languages
            .Where(l => l.LanguageId == request.Id)
            .ProjectTo<LanguageGetByIdResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}