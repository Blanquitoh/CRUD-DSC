using FluentValidation;
using MediatR;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext dbContext,
    IValidatorFork<LanguageDeleteRequest, Language> validator) : IRequestHandler<LanguageDeleteRequest, bool>
{
    public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<LanguageDeleteRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var language = validator.GetData(validationContext)!;
        dbContext.Languages.Remove(language);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
