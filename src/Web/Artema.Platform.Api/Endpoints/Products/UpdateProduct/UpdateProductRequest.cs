using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Artema.Platform.Api.Endpoints.UpdateProduct;

public record UpdateProductRequest
{
    [FromRoute(Name = "id")]
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public long Pvp { get; init; }
    public Guid? CategoryId { get; init; }
}

public class UpdateProductRequestValidator : Validator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Pvp).GreaterThan(0);
        When(x => x.CategoryId is not null, () =>
        {
            RuleFor(x => x.CategoryId)
                .NotEmpty();
        });
    }
}
