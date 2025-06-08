using FluentValidation;

namespace Sakila.Application.Common.Validation;

public interface IValidatorWithData<in TRequest, TData> : IValidator<TRequest>
{
    Task<TData> ValidateAndGetDataAsync(TRequest request,
        CancellationToken cancellationToken = default);
}
