using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Commands.Validators.Data;

public class CountryDeleteValidatorData(Country country)
{
    public Country Country { get; } = country;
}