using FluentValidation;

namespace Sakila.Application.Common.Validation;

/// <summary>
/// Allows validators to store entities in the <see cref="ValidationContext{T}"/>
/// so handlers can retrieve them after validation completes.
/// </summary>
/// <typeparam name="TRequest">Request type being validated.</typeparam>
/// <typeparam name="TData">Type of the entity loaded during validation.</typeparam>
public interface IValidatorFork<TRequest, TData> : IValidator<TRequest>
{
    /// <summary>
    /// Retrieves the stored entity from the given validation context.
    /// </summary>
    /// <param name="context">Validation context used during validation.</param>
    /// <returns>The entity if found; otherwise <c>null</c>.</returns>
    TData? GetData(ValidationContext<TRequest> context);
}

/// <inheritdoc/>
public abstract class ValidatorFork<TRequest, TData> : AbstractValidator<TRequest>, IValidatorFork<TRequest, TData>
{
    private readonly string _dataKey = $"{typeof(TRequest).FullName}:{typeof(TData).FullName}";

    protected void SetData(ValidationContext<TRequest> context, TData data)
        => context.RootContextData[_dataKey] = data!;

    public TData? GetData(ValidationContext<TRequest> context)
        => context.RootContextData.TryGetValue(_dataKey, out var value) ? (TData?)value : default;
}
