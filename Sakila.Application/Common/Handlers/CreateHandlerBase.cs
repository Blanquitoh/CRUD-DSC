using AutoMapper;
using FluentValidation;
using MediatR;
using Sakila.Infrastructure.Data;

namespace Sakila.Application.Common.Handlers;

public abstract class CreateHandlerBase<TRequest, TEntity, TResponse>(
    SakilaContext dbContext,
    IMapper mapper,
    IValidator<TRequest> validator) : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TEntity : class
{
    protected abstract TResponse GetResponse(TEntity entity);

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var entity = mapper.Map<TEntity>(request);
        dbContext.Set<TEntity>().Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        return GetResponse(entity);
    }
}