using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artema.Platform.Infrastructure.Data.TableModels;

public class ProductCategoryTableModel : AuditableBaseModel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public bool IsService { get; set; }

    public ICollection<ProductTableModel> Products { get; set; } = default!;
}

public class ProductCategoryTableConfiguration
    : AuditableBaseModelConfiguration<ProductCategoryTableModel>
{
    public override void Configure(EntityTypeBuilder<ProductCategoryTableModel> builder)
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
