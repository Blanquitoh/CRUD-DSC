using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Sakila.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
{
    private readonly RequestDelegate _next = next;
    private readonly ProblemDetailsFactory _problemDetailsFactory = problemDetailsFactory;
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in ex.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var problem = _problemDetailsFactory.CreateValidationProblemDetails(
                context,
                modelState,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Validation failed",
                type: "https://tools.ietf.org/html/rfc7231#section-6.5.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = _problemDetailsFactory.CreateProblemDetails(
                context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "A database error occurred.",
                detail: sqlEx.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (SqlException ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = _problemDetailsFactory.CreateProblemDetails(
                context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "A database error occurred.",
                detail: ex.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problem = _problemDetailsFactory.CreateProblemDetails(
                context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "An internal server error occurred.",
                detail: ex.Message,
                type: "https://tools.ietf.org/html/rfc7231#section-6.6.1");

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}