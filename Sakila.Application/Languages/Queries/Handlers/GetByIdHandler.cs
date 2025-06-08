using AutoMapper;
using FluentValidation;
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
        var validationContext = new ValidationContext<LanguageGetByIdRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var language = validator.GetData(validationContext);
        return mapper.Map<LanguageGetByIdResponse>(language);
    }
}
