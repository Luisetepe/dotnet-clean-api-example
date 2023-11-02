using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Artema.Platform.Api.Endpoints.DeleteProduct;

public record DeleteProductRequest
{
    [FromRoute]
    public Guid Id { get; init; }
}

public class DeleteProductRequestValidator : Validator<DeleteProductRequest>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
