using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handlers;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error Message: {exceptionMessage}, Time of occurence {time}", exception.Message,DateTime.UtcNow);
        
        (string Detail, string Title, int StatusCode) details = exception switch
        {
            InternalServerException e => (e.Message, e.GetType().Name, StatusCodes.Status500InternalServerError),
            ValidationException e => (e.Message, e.GetType().Name, StatusCodes.Status400BadRequest),
            BadRequestException e => (e.Message, e.GetType().Name, StatusCodes.Status400BadRequest),
            NotFoundException e => (e.Message, e.GetType().Name, StatusCodes.Status404NotFound),
            _ => (exception.Message, exception.GetType().Name, StatusCodes.Status500InternalServerError)
        };
        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.StatusCode,
            Instance = context.TraceIdentifier,
        };
        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("errors", validationException.Errors);
        }
        context.Response.StatusCode = details.StatusCode;
        await context.Response.WriteAsJsonAsync(problemDetails,cancellationToken);
        return true;
    }
}