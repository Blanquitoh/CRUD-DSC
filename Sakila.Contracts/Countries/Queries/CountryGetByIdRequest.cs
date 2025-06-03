using MediatR;
using Sakila.Contracts.Countries.Responses;

namespace Sakila.Contracts.Countries.Queries;

public class CountryGetByIdRequest : IRequest<CountryGetByIdResponse?>
{
    public int Id { get; set; }
}
