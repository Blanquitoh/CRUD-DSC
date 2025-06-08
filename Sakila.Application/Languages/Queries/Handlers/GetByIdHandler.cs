using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Languages.Queries.Validators.Data;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Application.Languages.Queries.Handlers;

public class GetByIdHandler(
    IMapper mapper,
    IValidatorWithData<LanguageGetByIdRequest, LanguageGetByIdValidatorData> validator)
    : IRequestHandler<LanguageGetByIdRequest, LanguageGetByIdResponse?>
{
    public async Task<LanguageGetByIdResponse?> Handle(LanguageGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        return mapper.Map<LanguageGetByIdResponse>(data.Language);
    }
}