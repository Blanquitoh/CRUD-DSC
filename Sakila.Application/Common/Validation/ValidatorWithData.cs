using FluentValidation;

namespace Sakila.Application.Common.Validation;

public abstract class ValidatorWithData<TRequest, TData> : AbstractValidator<TRequest>,
    IValidatorWithData<TRequest, TData>
{
    private readonly string _dataKey = $"{typeof(TRequest).FullName}:{typeof(TData).FullName}";

    public TData GetData(ValidationContext<TRequest> context)
    {
        return (context.RootContextData.TryGetValue(_dataKey, out var value) ? (TData)value : default)!;
    }

    protected void SetData(ValidationContext<TRequest> context, TData data)
    {
        context.RootContextData[_dataKey] = data!;
    }

    public async Task<TData> ValidateAndGetDataAsync(TRequest request,
        CancellationToken cancellationToken = default)
    {
        var context = new ValidationContext<TRequest>(request);
        var result = await ValidateAsync(context, cancellationToken);

        if (!result.IsValid) throw new ValidationException(result.Errors);

        return GetData(context);
    }
}
