using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Sakila.API.Middleware;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ProblemDetailsFactory problemDetailsFactory,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in ex.Errors) modelState.AddModelError(error.PropertyName, error.ErrorMessage);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problem = problemDetailsFactory.CreateValidationProblemDetails(
                context,
                modelState,
                StatusCodes.Status400BadRequest,
                "Validation failed",
                "https://tools.ietf.org/html/rfc7231#section-6.5.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            logger.LogError(sqlEx, "Database update error");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "A database error occurred.",
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (SqlException ex)
        {
            logger.LogError(ex, "Database error");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "A database error occurred.",
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "An internal server error occurred.",
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}