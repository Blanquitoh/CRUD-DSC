using MediatR;
using Sakila.Application.Common.Validation;
using Sakila.Application.Common.Interfaces;

namespace Sakila.Application.Common.Handlers;

public abstract class DeleteHandlerBase<TRequest, TEntity, TData>(
    ISakilaContext dbContext,
    IValidatorWithData<TRequest, TData> validator) : IRequestHandler<TRequest, bool>
    where TRequest : IRequest<bool> where TEntity : class
{
    public async Task<bool> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var data = await validator.ValidateAndGetDataAsync(request, cancellationToken);
        var entity = GetData(data);
        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    protected abstract TEntity GetData(TData data);
}