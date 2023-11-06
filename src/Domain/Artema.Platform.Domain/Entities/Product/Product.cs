using Artema.Platform.Domain.ValueObjects;
using NodaTime;

namespace Artema.Platform.Domain.Entities;

public class Product : Entity<EntityId>
{
    public ProductName Name { get; private set; }
    public ProductPvp Pvp { get; private set; }
    public EntityId? CategoryId { get; private set; }
    public Instant CreatedAt { get; }

    private Product(EntityId id, ProductName name, ProductPvp pvp, EntityId? categoryId, Instant createDate) : base(id)
    {
        Name = name;
        Pvp = pvp;
        CategoryId = categoryId;
        CreatedAt = createDate;
    }

    public static Product FromPrimitives(Guid id, string name, long pvp, Guid? categoryId, Instant createDate)
    {
        return new Product
        (
            EntityId.FromValue(id),
            ProductName.FromValue(name),
            ProductPvp.FromValue(pvp),
            categoryId is not null ? EntityId.FromValue(categoryId.Value) : null,
            createDate
        );
    }

    public void UpdateData(string name, long pvp, Guid? categoryId)
    {
        Name = ProductName.FromValue(name);
        Pvp = ProductPvp.FromValue(pvp);
        CategoryId = categoryId is not null ? EntityId.FromValue(categoryId.Value) : null;
    }
}
