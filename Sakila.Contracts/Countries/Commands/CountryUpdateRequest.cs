using MediatR;

namespace Sakila.Contracts.Countries.Commands;

public class CountryUpdateRequest : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
