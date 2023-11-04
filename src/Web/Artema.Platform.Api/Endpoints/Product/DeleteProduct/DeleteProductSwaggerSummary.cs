using Artema.Platform.Api.Models;
using FastEndpoints;

namespace Artema.Platform.Api.Endpoints.DeleteProduct;

public class DeleteProductSwaggerSummary : Summary<DeleteProductEndpoint>
{
    public DeleteProductSwaggerSummary()
    {
        Summary = "Deletes a product";
        ExampleRequest = new DeleteProductRequest
        {
            Id = Guid.NewGuid()
        };
        Response(204, "Product was successfully deleted.");
        Response(400, "Produced when the provided request is not valid.", "application/problem+json", example: new ValidationExceptionResponse
        {
            StatusCode = 400,
            Type = "ValidationException",
            Message = "One or more validation errors occurred.",
            Errors = new Dictionary<string, string[]>
            {
                {"Id", new []{ "'Id' must not be empty." }}
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