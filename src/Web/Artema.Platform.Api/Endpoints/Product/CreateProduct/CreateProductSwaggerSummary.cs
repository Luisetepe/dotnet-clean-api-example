using Artema.Platform.Api.Models;
using FastEndpoints;
using NodaTime;

namespace Artema.Platform.Api.Endpoints.CreateProduct;

public class CreateProductSwaggerSummary : Summary<CreateProductEndpoint>
{
    public CreateProductSwaggerSummary()
    {
        Summary = "Creates a new product";
        ExampleRequest = new CreateProductRequest
        {
            Name = "CocaCola",
            Pvp = 1000,
            CategoryId = Guid.NewGuid()
        };
        Response(201, "Product is successfully created", example: new CreateProductResponse
        {
            Id = Guid.NewGuid(),
            Name = "CocaCola",
            Pvp = 1000,
            CategoryId = Guid.NewGuid(),
            CreatedAt = Instant.FromUtc(2021, 10, 1, 0, 0, 0)
        });
        Response(400, "Produced when the provided request is not valid.", "application/problem+json", example: new ValidationExceptionResponse
        {
            StatusCode = 400,
            Type = "ValidationException",
            Message = "One or more validation errors occurred.",
            Errors = new Dictionary<string, string[]>
            {
                {"Name", new []{ "'Name' must not be empty." }},
                {"Pvp", new []{ "'Pvp' must be greater than '0'." }},
                {"CategoryId", new []{ "'CategoryId' must not be empty." }}
            }
        });
    }
}
