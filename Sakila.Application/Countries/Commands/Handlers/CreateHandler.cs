using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class CreateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidator<CountryCreateRequest> validator) : IRequestHandler<CountryCreateRequest, int>
{
    public async Task<int> Handle(CountryCreateRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var country = mapper.Map<Country>(request);

        dbContext.Countries.Add(country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return country.CountryId;
    }
}