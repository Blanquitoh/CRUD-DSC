using MediatR;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageCreateRequest : IRequest<int>
{
    public string Name { get; init; } = string.Empty;
}