using FluentValidation;
using Sakila.Contracts.Common;

namespace Sakila.Web.Common;

public class ApiResponse<TResponse> : IApiResponse<TResponse>
{
    public ApiResponse()
    {
    }

    public ApiResponse(TResponse? data)
    {
        Data = data;
    }

    public ApiResponse(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public ApiResponse(string error)
    {
        GeneralErrors.Add(error);
    }

    public ApiResponse(IEnumerable<string> errors)
    {
        GeneralErrors.AddRange(errors);
    }

    public ApiResponse(ValidationException exception)
    {
        Errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
    }

    public Dictionary<string, string[]> Errors { get; set; } = new();
    public List<string> GeneralErrors { get; set; } = new();
    public bool IsSuccess => !Errors.Any() && !GeneralErrors.Any();
    public TResponse? Data { get; set; }
}