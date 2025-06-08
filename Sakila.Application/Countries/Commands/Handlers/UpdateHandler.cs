using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Contracts.Countries.Commands;
using Sakila.Domain.Models;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Countries.Commands.Handlers;

public class UpdateHandler(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<CountryUpdateRequest, Country> validator)
    : IRequestHandler<CountryUpdateRequest, Unit>
{
    public async Task<Unit> Handle(CountryUpdateRequest request, CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<CountryUpdateRequest>(request);
        var result = await validator.ValidateAsync(validationContext, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        var country = validator.GetData(validationContext);

        mapper.Map(request, country);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
