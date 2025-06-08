using Sakila.Domain.Models;

namespace Sakila.Application.Countries.Commands.Validators.Data;

public class CountryUpdateValidatorData(Country country)
{
    public Country Country { get; set; } = country;
}
