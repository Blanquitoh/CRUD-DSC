using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext context,
    IMapper mapper,
    IValidator<CountryUpdateRequest> validator)
    : IRequestHandler<CountryUpdateRequest, Unit>
{
    public async Task<Unit> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var country = (Country)validationResult.RootContextData["country"];

        mapper.Map(request, country);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}