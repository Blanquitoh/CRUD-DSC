using MediatR;
using Microsoft.EntityFrameworkCore;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers
{
    public class DeleteHandler(SakilaContext context) : IRequestHandler<LanguageDeleteRequest, bool>
    {
        public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
        {
            var language = await context.Languages.FirstAsync(l => l.LanguageId == request.Id, cancellationToken);
            context.Languages.Remove(language);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}