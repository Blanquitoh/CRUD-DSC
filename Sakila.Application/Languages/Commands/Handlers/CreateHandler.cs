using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class CreateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidator<LanguageCreateRequest> validator)
    : IRequestHandler<LanguageCreateRequest, int>
{
    public async Task<int> Handle(LanguageCreateRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var language = mapper.Map<Language>(request);

        dbContext.Languages.Add(language);
        await dbContext.SaveChangesAsync(cancellationToken);

        return language.LanguageId;
    }
}
