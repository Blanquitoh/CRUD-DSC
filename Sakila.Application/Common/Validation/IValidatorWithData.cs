using FluentValidation;

namespace Sakila.Application.Common.Validation;

public interface IValidatorWithData<TRequest, out TData> : IValidator<TRequest>
{
    TData GetData(ValidationContext<TRequest> context);
}
