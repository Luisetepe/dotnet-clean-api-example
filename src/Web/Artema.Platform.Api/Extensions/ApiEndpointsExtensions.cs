using System.Reflection;
using System.Text.Json.Serialization;
using Artema.Platform.Api.Middlewares;
using Artema.Platform.Api.Models;
using FastEndpoints;
using FastEndpoints.Swagger;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Artema.Platform.Api.Extensions;

public static class ApiEndpointsExtensions
{
    public static IServiceCollection AddApiEndpoints(this IServiceCollection services)
    {
        return services
            .AddFastEndpoints(options => options.Assemblies = new []{Assembly.GetExecutingAssembly()})
            .SwaggerDocument(options =>
            {
                options.DocumentSettings = settings =>
                {
                    settings.Title = "Artema Platform Api";
                    settings.Version = "v1";
                };
                options.AutoTagPathSegmentIndex = 2;
                options.ShortSchemaNames = true;
            });
    }

    public static IApplicationBuilder UseApiEndpoints(this IApplicationBuilder app)
    {
        return app
            .UseCustomExceptionHandler()
            .UseFastEndpoints(config =>
            {
                config.Errors.ResponseBuilder = (failures, _, statusCode) => new ValidationExceptionResponse
                {
                    StatusCode = statusCode,
                    Type = "ValidationException",
                    Message = "One or more validation errors ocurred.",
                    Errors = failures
                        .GroupBy(x => $"{char.ToLower(x.PropertyName[0])}{x.PropertyName[1..]}")
                        .Aggregate(new Dictionary<string, string[]>(), (errors, next) =>
                        {
                            errors[next.Key] = next.Select(x => x.ErrorMessage).ToArray();
                            return errors;
                        })
                };
                config.Errors.ProducesMetadataType = typeof(ValidationExceptionResponse);
                config.Serializer.Options.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
                config.Serializer.Options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .UseSwaggerGen(uiConfig: settings => settings.DefaultModelsExpandDepth = -1);
    }
}