using FluentValidation;
using MediatR;
using Sakila.Domain.Models;
using Sakila.Contracts.Languages.Commands;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class DeleteHandler(
    SakilaContext context,
    IValidator<LanguageDeleteRequest> validator) : IRequestHandler<LanguageDeleteRequest, bool>
{
    public async Task<bool> Handle(LanguageDeleteRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var ctx = new ValidationContext<LanguageDeleteRequest>(request);
        var language = (Language)ctx.RootContextData["language"];
        context.Languages.Remove(language);
        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}