using FluentValidation;
using Sakila.Web.Abstractions;

namespace Sakila.Web.Common;

public class SakilaSakilaApiResponse<TResponse> : ISakilaApiResponse<TResponse>
{
    public SakilaSakilaApiResponse()
    {
    }

    public SakilaSakilaApiResponse(TResponse? data)
    {
        Data = data;
    }

    public SakilaSakilaApiResponse(Dictionary<string, string[]> errors)
    {
        Errors = errors;
    }

    public SakilaSakilaApiResponse(string error)
    {
        GeneralErrors.Add(error);
    }

    public SakilaSakilaApiResponse(ValidationException exception)
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