namespace Sakila.Web.Common;

public class ValidationErrorResponse
{
    public Dictionary<string, string[]> Errors { get; init; } = new();
}