using Artema.Platform.Domain.ValueObjects;
using NodaTime;

namespace Artema.Platform.Domain.Entities.ProductCategory;

public class ProductCategory : Entity<EntityId>
{
    public CategoryName Name { get; private set; }
    public bool IsService { get; set; }
    public Instant CreatedAt { get; }

    private ProductCategory(EntityId id, CategoryName name, bool isService, Instant createDate) : base(id)
    {
        Name = name;
        IsService = isService;
        CreatedAt = createDate;
    }

    public static ProductCategory FromPrimitives(Guid id, string name, bool isService, Instant createDate)
    {
        return new ProductCategory
        (
            EntityId.FromValue(id),
            CategoryName.FromValue(name),
            isService,
            createDate
        );
    }
}
