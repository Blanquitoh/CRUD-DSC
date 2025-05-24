using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sakila.Contracts.Languages.Commands;
using Sakila.Contracts.Languages.Queries;
using Sakila.Contracts.Languages.Responses;

namespace Sakila.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<LanguageGetAllResponse>> GetAll()
    {
        var languages = await mediator.Send(new LanguageGetAllRequest());
        return Ok(languages);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LanguageGetByIdResponse>> GetById(int id)
    {
        var language = await mediator.Send(new LanguageGetByIdRequest { Id = id });
        return language == null ? NotFound() : Ok(language);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLanguage(int id, LanguageUpdateRequest command)
    {
        if (id != command.Id) return BadRequest();

        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> PostLanguage(LanguageCreateRequest command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, command);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        var result = await mediator.Send(new LanguageDeleteRequest { Id = id });
        return result ? NoContent() : NotFound();
    }
}