using MediatR;

namespace Sakila.Contracts.Countries.Commands;

public class CountryCreateRequest : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
}