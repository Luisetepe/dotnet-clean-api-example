using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artema.Platform.Infrastructure.Data.TableModels;

public class ProductCategoryTable : AuditableBaseModel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public bool IsService { get; set; }

    public ICollection<ProductTable> Products { get; set; } = default!;
}

public class ProductCategoryTableConfiguration
    : AuditableBaseModelConfiguration<ProductCategoryTable>
{
    public override void Configure(EntityTypeBuilder<ProductCategoryTable> builder)
    {
        base.Configure(builder);

        builder.ToTable("PRODUCT_CATEGORIES");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.IsService)
            .HasColumnType("boolean")
            .IsRequired();
    }
}