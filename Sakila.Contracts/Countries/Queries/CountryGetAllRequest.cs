using MediatR;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.Contracts.Countries.Queries;

public class CountryGetAllRequest : IRequest<CountryGetAllResponse>;
