using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class UpdateHandler(
    SakilaContext context,
    IMapper mapper,
    IValidatorFork<LanguageUpdateRequest, Language> validator)
    : IRequestHandler<LanguageUpdateRequest, Unit>
{
    public async Task<Unit> Handle(LanguageUpdateRequest request, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<LanguageUpdateRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var language = validator.GetData(validationContext)!;
        mapper.Map(request, language);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
