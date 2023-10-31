using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artema.Platform.Infrastructure.Data.TableModels;

public class ProductTable : AuditableBaseModel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public long Pvp { get; set; }
    public Guid? CategoryId { get; set; }

    public ProductCategoryTable? Category { get; set; }
}

public class ProductTableConfiguration
    : AuditableBaseModelConfiguration<ProductTable>
{
    public override void Configure(EntityTypeBuilder<ProductTable> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("PRODUCTS");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.Pvp)
            .HasColumnType("bigint")
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(pc => pc.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}