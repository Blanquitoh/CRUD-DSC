using MediatR;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Contracts.Languages.Queries;

public class LanguageGetByIdRequest : IRequest<LanguageGetByIdResponse?>
{
    public int Id { get; set; }
}
