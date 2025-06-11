using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Sakila.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
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
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "A database error occurred.",
                detail: sqlEx.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (SqlException ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "A database error occurred.",
                detail: ex.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = problemDetailsFactory.CreateProblemDetails(
                context,
                StatusCodes.Status500InternalServerError,
                "An internal server error occurred.",
                detail: ex.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}