using MediatR;

namespace Sakila.Contracts.Countries.Commands;

public class CountryUpdateRequest : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}