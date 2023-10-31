using Artema.Platform.Api.Models;
using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.CreateProduct;

public class CreateProductSwaggerSummary : Summary<CreateProductEndpoint>
{
    public CreateProductSwaggerSummary()
    {
        Summary = "Create a new product";
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
            CreatedAt = DateTime.UtcNow
        });
        Response(400, "Produced when the provided request is not valid.", "application/problem+json", example: new ValidationExceptionResponse
        {
            StatusCode = 400,
            Type = "ValidationException",
            Message = "One or more validation errors occurred.",
            Errors = new Dictionary<string, string[]>
            {
                {"Name", new []{ "Value must be not empty." }},
                {"Pvp", new []{ "Value must be greater than 0." }},
                {"CategoryId", new []{ "Value must be not empty." }}
            }
        });
    }
}