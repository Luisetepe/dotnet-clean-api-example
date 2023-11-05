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

        builder.ToTable("product_categories");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.IsService)
            .HasColumnName("is_service")
            .HasColumnType("boolean")
            .IsRequired();
    }
}
