using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sakila.Contracts.Countries.Commands;
using Sakila.Contracts.Countries.Queries;
using Sakila.Contracts.Countries.Queries.Responses;

namespace Sakila.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CountryGetAllResponse>> GetAllAsync()
    {
        var countries = await mediator.Send(new CountryGetAllRequest());
        return Ok(countries);
    }

    [HttpGet("{id}", Name = "GetCountryById")]
    public async Task<ActionResult<CountryGetByIdResponse>> GetByIdAsync(int id)
    {
        var country = await mediator.Send(new CountryGetByIdRequest { Id = id });
        return country == null ? NotFound() : Ok(country);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountryAsync(int id, CountryUpdateRequest command)
    {
        if (id != command.Id) return BadRequest();

        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> PostCountryAsync(CountryCreateRequest command)
    {
        var id = await mediator.Send(command);
        return CreatedAtRoute("GetCountryById", new { id }, command);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountryAsync(int id)
    {
        var result = await mediator.Send(new CountryDeleteRequest { Id = id });
        return result ? NoContent() : NotFound();
    }
}