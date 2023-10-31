using Artema.Platform.Api.Models;
using FastEndpoints;
using NodaTime;

namespace Artema.Platform.Api.Endpoints.GetProductById;

public class GetProductByIdSwaggerSummary : Summary<GetProductByIdEndpoint>
{
    public GetProductByIdSwaggerSummary()
    {
        Summary = "Get a Product by its Id.";
        ExampleRequest = new GetProductByIdRequest
        {
            Id = Guid.NewGuid()
        };
        Response(200, "The Product found by given Id.", example: new GetProductByIdResponse
        {
            Id = Guid.NewGuid(),
            Name = "CocaCola",
            Pvp = 2600,
            CategoryId = Guid.NewGuid(),
            CreatedAt = Instant.FromUtc(2021, 10, 1, 0, 0)
        });
        Response(404, "Produced when the Product with given Id is not found.", "application/problem+json", example: new ExceptionHttpResponse
        {
            StatusCode = 404,
            Type = "EntityNotFoundException",
            Message = "The Product with given Id was not found."
        });
        Response(400, "Produced when the provided Id is not valid.", "application/problem+json", example: new ValidationExceptionResponse
        {
            StatusCode = 400,
            Type = "ValidationException",
            Message = "One or more validation errors occurred.",
            Errors = new Dictionary<string, string[]>
            {
                {"Id", new []{ "Value must be not empty." }}
            }
        });
    }
}