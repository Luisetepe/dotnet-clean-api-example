using NodaTime;

namespace Artema.Platform.Application.UseCases.Queries.GetProductById;

public record GetProductByIdQueryResponse
{
    public record Product
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public decimal Pvp { get; init; }
        public Guid? CategoryId { get; init; }
        public Instant CreatedAt { get; init; }
    }
    
    public Product? ProductData { get; init; }
}