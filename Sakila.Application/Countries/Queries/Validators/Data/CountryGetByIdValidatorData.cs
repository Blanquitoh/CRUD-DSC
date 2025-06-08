using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Queries.Validators.Data;

public class CountryGetByIdValidatorData(Country country)
{
    public Country Country { get; } = country;
}