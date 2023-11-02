
using FastEndpoints;
using FluentValidation;

namespace Artema.Platform.Api.Endpoints.CreateProduct;

public record CreateProductRequest
{
    public string Name { get; init; } = default!;
    public long Pvp { get; init; }
    public Guid? CategoryId { get; init; }
}

public class CreateProductRequestValidator : Validator<CreateProductRequest>
{
    public CreateProductRequestValidator()
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