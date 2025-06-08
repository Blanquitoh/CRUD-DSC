using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Commands.Validators.Data;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorWithData<LanguageDeleteRequest, LanguageDeleteValidatorData> validator)
    : IRequestHandler<LanguageDeleteRequest, bool>
{
    public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        dbContext.Languages.Remove(data.Language);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}