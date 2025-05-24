using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class UpdateHandler(SakilaContext context, IMapper mapper)
    : IRequestHandler<LanguageUpdateRequest, Unit>
{
    public async Task<Unit> Handle(LanguageUpdateRequest request, CancellationToken cancellationToken)
    {
        var language = await context.Languages
            .FirstAsync(l => l.LanguageId == request.Id, cancellationToken);

        mapper.Map(request, language);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}