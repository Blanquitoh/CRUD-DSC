using MediatR;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageUpdateRequest : IRequest<Unit>
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}