using AutoMapper;
using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Common.Handlers;

public abstract class UpdateHandlerBase<TRequest, TEntity, TData>(
    SakilaContext dbContext,
    IMapper mapper,
    IValidatorWithData<TRequest, TData> validator) : IRequestHandler<TRequest, Unit>
    where TRequest : IRequest<Unit>
{
    protected abstract TEntity GetData(TData data);

    public async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        var entity = GetData(data);
        mapper.Map(request, entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
