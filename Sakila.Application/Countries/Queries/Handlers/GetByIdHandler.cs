using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Countries.Queries.Validators.Data;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Application.Countries.Queries.Handlers;

public class GetByIdHandler(
    IMapper mapper,
    IValidatorWithData<CountryGetByIdRequest, CountryGetByIdValidatorData> validator)
    : IRequestHandler<CountryGetByIdRequest, CountryGetByIdResponse?>
{
    public async Task<CountryGetByIdResponse?> Handle(CountryGetByIdRequest request,
        CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        return mapper.Map<CountryGetByIdResponse>(data.Country);
    }
}