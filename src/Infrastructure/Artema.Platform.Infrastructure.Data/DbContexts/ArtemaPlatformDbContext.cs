using System.Reflection;
using Artema.Platform.Infrastructure.Data.TableModels;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Infrastructure.Data.DbContexts;

public class ArtemaPlatformDbContext : DbContext
{
    public ArtemaPlatformDbContext(DbContextOptions options) : base(options) { }

    public DbSet<ProductTableModel> Products { get; set; } = default!;
    public DbSet<ProductCategoryTableModel> ProductCategories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
