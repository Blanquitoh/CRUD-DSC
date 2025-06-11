using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Common.Handlers;

public abstract class DeleteHandlerBase<TRequest, TEntity, TData>(
    SakilaContext dbContext,
    IValidatorWithData<TRequest, TData> validator) : IRequestHandler<TRequest, bool>
    where TRequest : IRequest<bool>
{
    protected abstract TEntity GetData(TData data);

    public async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        var entity = GetData(data);
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
