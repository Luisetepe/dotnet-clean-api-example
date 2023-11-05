using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace Artema.Platform.Infrastructure.Data.TableModels;

public abstract class AuditableBaseModel
{
    public Instant CreatedAt { get; init; }
    public Instant? UpdatedAt { get; set; }
}

public abstract class AuditableBaseModelConfiguration<TModel>
    : IEntityTypeConfiguration<TModel> where TModel : AuditableBaseModel
{
    public virtual void Configure(EntityTypeBuilder<TModel> builder)
    {
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamptz")
            .IsRequired(false);
    }
}