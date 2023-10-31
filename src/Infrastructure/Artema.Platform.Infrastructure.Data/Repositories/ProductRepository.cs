using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Repositories;
using Artema.Platform.Infrastructure.Data.CriteriaEngines;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Exceptions;
using Artema.Platform.Infrastructure.Data.TableModels;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Infrastructure.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ArtemaPlatformDbContext _dbContext;

    public ProductRepository(ArtemaPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Product>> SearchProducts(SearchCriteria criteria, CancellationToken ct = default)
    {
        var query = EntityFrameworkCriteriaEngine.ApplyCriteria(_dbContext.Products.AsNoTracking(), criteria);
        var result = await query
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .ToListAsync(ct);

        return result;
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken ct = default)
    {
        var result = await _dbContext.Products
            .AsNoTracking()
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .ToListAsync(ct);

        return result;
    }

    public async Task<Product?> GetProductById(Guid id, CancellationToken ct = default)
    {
        var result = await _dbContext.Products
            .AsNoTracking()
            .Where(table => table.Id == id)
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<Product> CreateProduct(Product product, CancellationToken ct = default)
    {
        if
        (
            product.CategoryId is not null
            && !await _dbContext.ProductCategories.AnyAsync(table => table.Id == product.CategoryId.Value, ct)
        )
        {
            throw new RelationNotFoundException(nameof(Product), nameof(product.CategoryId), product.CategoryId!.Value.ToString());
        }

        var table = new ProductTable
        {
            Id = product.Id.Value,
            Name = product.Name.Value,
            Pvp = product.Pvp.Value,
            CategoryId = product.CategoryId?.Value,
            CreatedAt = product.CreateDate
        };

        await _dbContext.Products.AddAsync(table, ct);
        await _dbContext.SaveChangesAsync(ct);

        return product;
    }

}