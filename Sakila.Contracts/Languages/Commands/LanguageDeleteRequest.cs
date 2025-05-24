using MediatR;

namespace Sakila.Contracts.Languages.Commands;

public class LanguageDeleteRequest : IRequest<bool>
{
    public int Id { get; set; }
}