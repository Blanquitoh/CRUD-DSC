using MediatR;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Contracts.Countries.Queries;

public class CountryGetByIdRequest : IRequest<CountryGetByIdResponse?>
{
    public int Id { get; init; }
}