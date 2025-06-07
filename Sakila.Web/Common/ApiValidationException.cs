namespace Sakila.Web.Common;

public class ApiValidationException(Dictionary<string, string[]> errors) : Exception("Validation failed")
{
    public Dictionary<string, string[]> Errors { get; } = errors;
}
