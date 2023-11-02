using Artema.Platform.Api.Models;
using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.UpdateProduct;

public class UpdateProductSwaggerSummary : Summary<UpdateProductEndpoint>
{
    public UpdateProductSwaggerSummary()
    {
        Summary = "Updates a product";
        ExampleRequest = new UpdateProductRequest
        {
            Id = Guid.NewGuid(),
            Name = "Cocacola modified",
            Pvp = 10500
        };
        Response(204, "Product updated");
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
        Response(404, "Produced when the Product with given Id is not found.", "application/problem+json", example: new ExceptionHttpResponse
        {
            StatusCode = 404,
            Type = "EntityNotFoundException",
            Message = "Could not find entity 'Product' by property 'Id' with value 'a7695b3d-4149-5e4f-b1f4-4fcca1e4bf58'"
        });
    }
}
