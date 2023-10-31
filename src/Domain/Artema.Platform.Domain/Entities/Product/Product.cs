using Artema.Platform.Domain.ValueObjects;
using NodaTime;

namespace Artema.Platform.Domain.Entities;

public class Product
{
    public EntityId Id { get; }
    public ProductName Name { get; }
    public ProductPvp Pvp { get; private set; }
    public EntityId? CategoryId { get; private set; }
    public Instant CreateDate { get; }

    private Product(EntityId id, ProductName name, ProductPvp pvp, EntityId? categoryId, Instant createDate)
    {
        Id = id;
        Name = name;
        Pvp = pvp;
        CategoryId = categoryId;
        CreateDate = createDate;
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
}