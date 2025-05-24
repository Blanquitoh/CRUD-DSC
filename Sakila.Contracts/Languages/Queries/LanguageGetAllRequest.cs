using MediatR;
using Sakila.Contracts.Languages.Responses;

namespace Sakila.Contracts.Languages.Queries;

public class LanguageGetAllRequest : IRequest<LanguageGetAllResponse>;