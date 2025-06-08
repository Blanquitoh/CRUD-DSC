using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<LanguageUpdateRequest, Language> validator)
    : IRequestHandler<LanguageUpdateRequest, Unit>
{
    public async Task<Unit> Handle(LanguageUpdateRequest request, CancellationToken cancellationToken)
    {
        var language = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        mapper.Map(request, language);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}