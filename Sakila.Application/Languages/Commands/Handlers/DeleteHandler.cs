using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<LanguageDeleteRequest, Language> validator) : IRequestHandler<LanguageDeleteRequest, bool>
{
    public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
    {
        var language = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        dbContext.Languages.Remove(language);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}