using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artema.Platform.Infrastructure.Data.TableModels;

public class ProductTableModel : AuditableBaseModel
{
    public Guid Id { get; init; }
    public string Name { get; set; } = default!;
    public long Pvp { get; set; }
    public Guid? CategoryId { get; set; }

    public ProductCategoryTableModel? Category { get; set; }
}

public class ProductTableConfiguration
    : AuditableBaseModelConfiguration<ProductTableModel>
{
    public override void Configure(EntityTypeBuilder<ProductTableModel> builder)
    {
        base.Configure(builder);

        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(p => p.Pvp)
            .HasColumnName("pvp")
            .HasColumnType("bigint")
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id")
            .HasColumnType("uuid");

        builder.HasOne(p => p.Category)
            .WithMany(pc => pc.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
