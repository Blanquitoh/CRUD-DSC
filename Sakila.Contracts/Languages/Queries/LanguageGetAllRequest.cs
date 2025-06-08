using MediatR;
using Sakila.Contracts.Languages.Queries.Responses;

namespace Sakila.Contracts.Languages.Queries;

public class LanguageGetAllRequest : IRequest<LanguageGetAllResponse>;