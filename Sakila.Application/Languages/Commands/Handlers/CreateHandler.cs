using AutoMapper;
using MediatR;
using Sakila.Contracts.Languages.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Commands.Handlers;

public class CreateHandler(
    SakilaContext context,
    IMapper mapper)
    : IRequestHandler<LanguageCreateRequest, int>
{
    public async Task<int> Handle(LanguageCreateRequest request, CancellationToken cancellationToken)
    {
        var language = mapper.Map<Language>(request);

        context.Languages.Add(language);
        await context.SaveChangesAsync(cancellationToken);

        return language.LanguageId;
    }
}