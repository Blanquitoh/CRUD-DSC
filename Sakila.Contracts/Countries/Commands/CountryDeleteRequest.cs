using MediatR;

namespace Sakila.Contracts.Countries.Commands;

public class CountryDeleteRequest : IRequest<bool>
{
    public int Id { get; init; }
}