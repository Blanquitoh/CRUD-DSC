using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Queries.Responses;
using Sakila.Domain.Models;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Queries.Handlers;

public class GetByIdHandler(
    SakilaContext context,
    IMapper mapper,
    IValidatorFork<CountryGetByIdRequest, Country> validator)
    : IRequestHandler<CountryGetByIdRequest, CountryGetByIdResponse?>
{
    public async Task<CountryGetByIdResponse?> Handle(CountryGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<CountryGetByIdRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var country = validator.GetData(validationContext)!;
        return mapper.Map<CountryGetByIdResponse>(country);
    }
}
