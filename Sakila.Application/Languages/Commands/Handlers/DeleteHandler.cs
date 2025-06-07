using FluentValidation;
using MediatR;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidator<LanguageDeleteRequest> validator) : IRequestHandler<LanguageDeleteRequest, bool>
{
    public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<LanguageDeleteRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var language = (Language)validationContext.RootContextData["language"];
        dbContext.Languages.Remove(language);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}