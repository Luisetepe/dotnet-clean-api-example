using Artema.Platform.Api.Models;
using Artema.Platform.Domain.Exceptions;
using Artema.Platform.Infrastructure.Data.Exceptions;
using FastEndpoints;

namespace Artema.Platform.Api.Middlewares;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

class ExceptionHandler { }

public static class ExceptionHandlerExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app, ILogger? logger = null, bool logStructuredException = false)
    {
        app.UseExceptionHandler(
            errApp =>
            {
                errApp.Run(
                    async ctx =>
                    {
                        var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();

                        if (exHandlerFeature is not null)
                        {
                            logger ??= ctx.Resolve<ILogger<ExceptionHandler>>();
                            var http = exHandlerFeature.Endpoint?.DisplayName?.Split(" => ")[0];
                            var type = exHandlerFeature.Error.GetType().Name;
                            var error = exHandlerFeature.Error.Message;
                            if (logStructuredException)
                            {
                                // ReSharper disable once ExceptionPassedAsTemplateArgumentProblem
                                logger.LogError("{@Http}{@Type}{@Reason}{@Exception}", http, type, error,
                                    exHandlerFeature.Error);
                            }
                            else
                            {
                                var msg =
                                    $"""
                                     =================================
                                     {http}
                                     TYPE: {type}
                                     REASON: {error}
                                     =================================
                                     """;

                                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                                logger.LogError(msg);
                            }

                            await SendResponseFromException(ctx, exHandlerFeature.Error);
                        }
                    });
            });

        return app;
    }

    private static Task SendResponseFromException(HttpContext ctx, Exception ex)
    {
        ctx.Response.ContentType = "application/problem+json";

        var responseTask = ex switch
        {
            InvalidCriteriaException or InvalidColumnException or RelationNotFoundException => BuildInvalidInputResponse(ctx, ex),
            _ => BuildInternalErrorResponse(ctx)
        };

        return responseTask;
    }

    private static Task BuildInvalidInputResponse(HttpContext ctx, Exception ex)
    {
        ctx.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        return ctx.Response.WriteAsJsonAsync
        (
            new ExceptionHttpResponse
            {
                StatusCode = ctx.Response.StatusCode,
                Type = ex.GetType().Name,
                Message = ex.Message
            }
        );
    }

    private static Task BuildInternalErrorResponse(HttpContext ctx)
    {
        ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return ctx.Response.WriteAsJsonAsync
        (
            new ExceptionHttpResponse
            {
                StatusCode = ctx.Response.StatusCode,
                Type = "UnhandledException",
                Message = "An error ocurred while processing the request."
            }
        );
    }

    private static Task BuildNotFoundResponse(HttpContext ctx, Exception ex)
    {
        ctx.Response.StatusCode = (int)HttpStatusCode.NotFound;
        return ctx.Response.WriteAsJsonAsync
        (
            new ExceptionHttpResponse
            {
                StatusCode = ctx.Response.StatusCode,
                Type = ex.GetType().Name,
                Message = ex.Message
            }
        );
    }
}