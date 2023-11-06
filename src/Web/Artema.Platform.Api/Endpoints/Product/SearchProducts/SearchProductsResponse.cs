using NodaTime;

namespace Artema.Platform.Api.Endpoints.SearchProducts;

public record SearchProductsResponse
{
    public record Product
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public long Pvp { get; init; }
        public Guid? CategoryId { get; init; }
        public Instant CreatedAt { get; init; }
    }

    public IEnumerable<Product> Products { get; init; } = Array.Empty<Product>();
}