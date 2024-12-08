using System.Text.Json;
using FluentValidation;
using Issuel.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Personnel.Api.Common.Dto;

namespace Issuel.Api.Common.Middleware;

/// <summary>
/// Глобальный обработчик исключений.
/// </summary>
public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException validationException)
        {
            var problemDetails = CreateProblemDetails(validationException);

            await WriteResponseAsync(context, (int) problemDetails.Status!, problemDetails);
        }
        catch (Exception exception) when (exception is EntityNotFoundException or EntityAlreadyExistsException)
        {
            var problemDetails = CreateProblemDetails(exception);

            await WriteResponseAsync(context, (int) problemDetails.Status!, problemDetails);
        }
        catch (Exception exception)
        {
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message,
                Data = exception.Data,
                StackTrace = exception.StackTrace,
                Type = exception.GetType().Name,
                StatusCode = StatusCodes.Status500InternalServerError
            };

            await WriteResponseAsync(context, errorResponse.StatusCode, errorResponse);
        }
    }

    private ProblemDetails CreateProblemDetails(Exception exception)
    {
        var statusCode = exception switch
        {
            EntityNotFoundException or FileNotFoundException => StatusCodes.Status404NotFound,
            EntityAlreadyExistsException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };

        return new ProblemDetails
        {
            Title = exception.GetType().Name,
            Status = statusCode,
            Detail = exception.Message
        };
    }

    private ValidationProblemDetails CreateProblemDetails(ValidationException exception)
    {
        var modelState = new ModelStateDictionary();

        foreach (var error in exception.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        var validationProblemDetails = new ValidationProblemDetails(modelState)
        {
            Status = StatusCodes.Status422UnprocessableEntity,
        };

        return validationProblemDetails;
    }

    private async Task WriteResponseAsync<T>(HttpContext context, int statusCode, T response)
    {
        context.Response.ContentType = "application/problem+json; charset=utf-8";
        context.Response.StatusCode = statusCode;

        JsonSerializerOptions? options = null;
        await context.Response.WriteAsJsonAsync(response, options, context.Response.ContentType, CancellationToken.None);
    }
}