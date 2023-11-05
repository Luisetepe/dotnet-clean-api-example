using Artema.Platform.Domain.Entities.ProductCategory;
using Artema.Platform.Domain.Repositories;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Infrastructure.Data.Repositories.EntityFramework;

public class ProductCategoryEfRepository : IProductCategoryRepository
{
    private readonly ArtemaPlatformDbContext _dbContext;

    public ProductCategoryEfRepository(ArtemaPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProductCategory>> GetAllProductCategories(CancellationToken ct)
    {
        var categories = await _dbContext.ProductCategories
            .Select(x => ProductCategory.FromPrimitives(x.Id, x.Name, x.IsService, x.CreatedAt))
            .ToListAsync(ct);

        return categories;
    }
}
