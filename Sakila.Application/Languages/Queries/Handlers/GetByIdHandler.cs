using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Domain.Models;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Responses;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetByIdHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<LanguageGetByIdRequest> validator)
    : IRequestHandler<LanguageGetByIdRequest, LanguageGetByIdResponse?>
{
    public async Task<LanguageGetByIdResponse?> Handle(LanguageGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var ctx = new ValidationContext<LanguageGetByIdRequest>(request);
        var language = (Language)ctx.RootContextData["language"];
        return mapper.Map<LanguageGetByIdResponse>(language);
    }
}