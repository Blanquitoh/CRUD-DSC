using System.Collections.Generic;

namespace Sakila.Contracts.Languages.Queries.Responses;

public class LanguageGetAllResponse
{
    public List<LanguageGetByIdResponse> Languages { get; set; } = new();
}
