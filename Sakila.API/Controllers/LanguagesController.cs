using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sakila.API.Data;
using Sakila.API.Models;

namespace Sakila.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguagesController(SakilaContext context, IValidator<Language> validator) : ControllerBase
{
    // GET: api/Languages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
    {
        return await context.Languages.ToListAsync();
    }

    // GET: api/Languages/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Language>> GetLanguage(int id)
    {
        var language = await context.Languages.FindAsync(id);

        if (language == null) return NotFound();

        return language;
    }

    // PUT: api/Languages/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutLanguage(int id, Language language)
    {
        if (id != language.LanguageId)
            return BadRequest();

        var result = await validator.ValidateAsync(language, opts => opts.IncludeRuleSets("Update"));
        if (!result.IsValid)
            return BadRequest(result.Errors);

        context.Entry(language).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LanguageExists(id))
                return NotFound();
            throw;
        }

        return NoContent();
    }

    // POST: api/Languages
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Language>> PostLanguage(Language language)
    {
        var result = await validator.ValidateAsync(language, opts => opts.IncludeRuleSets("Create"));
        if (!result.IsValid)
            return BadRequest(result.Errors);

        context.Languages.Add(language);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetLanguage", new { id = language.LanguageId }, language);
    }

    // DELETE: api/Languages/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        var language = await context.Languages.FindAsync(id);
        if (language == null) return NotFound();

        context.Languages.Remove(language);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private bool LanguageExists(int id)
    {
        return context.Languages.Any(e => e.LanguageId == id);
    }
}