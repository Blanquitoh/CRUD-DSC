using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Queries.Responses;
using Sakila.Domain.Models;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetByIdHandler(
    IMapper mapper,
    IValidatorWithData<LanguageGetByIdRequest, Language> validator)
    : IRequestHandler<LanguageGetByIdRequest, LanguageGetByIdResponse?>
{
    public async Task<LanguageGetByIdResponse?> Handle(LanguageGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var language = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        return mapper.Map<LanguageGetByIdResponse>(language);
    }
}