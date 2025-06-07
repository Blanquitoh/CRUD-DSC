using System.Collections.Generic;

namespace Sakila.Contracts.Common;

public interface IApiResponse<TResponse>
{
    Dictionary<string, string[]> Errors { get; set; }
    List<string> GeneralErrors { get; set; }
    bool IsSuccess { get; }
    TResponse? Data { get; set; }
}
