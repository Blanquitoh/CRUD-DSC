using FluentValidation;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Common;

public class SakilaApiResponse<TResponse> : ISakilaApiResponse<TResponse>
{
    public SakilaApiResponse()
    {
    }

    public SakilaApiResponse(TResponse? data)
    {
        Data = data;
    }

    public SakilaApiResponse(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public SakilaApiResponse(string error)
    {
        GeneralErrors.Add(error);
    }

    public SakilaApiResponse(ValidationException exception)
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