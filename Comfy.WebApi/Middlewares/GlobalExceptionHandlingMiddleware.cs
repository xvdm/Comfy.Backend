using Comfy.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using FluentValidation;

namespace Comfy.WebApi.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        Console.WriteLine(exception.Message);

        context.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ArgumentException or InvalidOperationException or ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };


        var message = exception.Message;
        if (exception is ValidationException)
        {
            var errors = exception.Message.Split('\n')[1..];
            var messages = errors.Select(x => x.Substring(x.IndexOf(':') + 2, x.IndexOf('.') - x.IndexOf(':') - 2));
            message = messages.Aggregate((x, y) => x + ". " + y);
        }

        ProblemDetails problemDetails = new()
        {
            Detail = message
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
    }
}