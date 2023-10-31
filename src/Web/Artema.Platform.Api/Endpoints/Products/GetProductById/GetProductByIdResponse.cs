using NodaTime;

namespace Artema.Platform.Api.Endpoints.GetProductById;

public record GetProductByIdResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
    public decimal Pvp { get; init; }
    public Guid? CategoryId { get; init; }
    public Instant CreatedAt { get; init; }
}