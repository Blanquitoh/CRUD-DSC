using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class UpdateHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<LanguageUpdateRequest> validator)
    : IRequestHandler<LanguageUpdateRequest, Unit>
{
    public async Task<Unit> Handle(LanguageUpdateRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var ctx = new ValidationContext<LanguageUpdateRequest>(request);
        var language = (Language)ctx.RootContextData["language"];

        mapper.Map(request, language);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}