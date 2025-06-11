namespace Sakila.Web.Abstractions;

public interface ISakilaApiResponse<TResponse>
{
    Dictionary<string, string[]> Errors { get; set; }
    List<string> GeneralErrors { get; set; }
    bool IsSuccess { get; }
    TResponse? Data { get; set; }
}