using NodaTime;

namespace Artema.Platform.Api.Endpoints.CreateProduct;

public record CreateProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public long Pvp { get; init; }
    public Guid? CategoryId { get; init; }
    public Instant CreatedAt { get; init; }
}