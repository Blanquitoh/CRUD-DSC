using MediatR;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageUpdateRequest : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}