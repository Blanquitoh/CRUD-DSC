using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class CreateHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<CountryCreateRequest> validator) : IRequestHandler<CountryCreateRequest, int>
{
    public async Task<int> Handle(CountryCreateRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var country = mapper.Map<Country>(request);

        context.Countries.Add(country);
        await context.SaveChangesAsync(cancellationToken);

        return country.CountryId;
    }
}