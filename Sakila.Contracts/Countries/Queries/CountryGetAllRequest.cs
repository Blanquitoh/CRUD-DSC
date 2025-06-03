using MediatR;
using Sakila.Contracts.Countries.Responses;

namespace Sakila.Contracts.Countries.Queries;

public class CountryGetAllRequest : IRequest<CountryGetAllResponse>;
