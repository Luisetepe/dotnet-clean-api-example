using Artema.Platform.Domain.Criteria;
using Artema.Platform.Domain.Entities;
using Artema.Platform.Domain.Repositories;
using Artema.Platform.Domain.ValueObjects;
using Artema.Platform.Infrastructure.Data.CriteriaEngines;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Exceptions;
using Artema.Platform.Infrastructure.Data.TableModels;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Infrastructure.Data.Repositories.EntityFramework;

public class ProductEfRepository : IProductRepository
{
    private readonly ArtemaPlatformDbContext _dbContext;

    public ProductEfRepository(ArtemaPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public SearchConfiguration GetSearchConfiguration()
    {
        var filterFields = new Dictionary<string, string[]>
        {
            { nameof(ProductTableModel.Id), new[] { "eq", "neq" } },
            { nameof(ProductTableModel.Name), new[] { "eq", "neq", "inc" } },
            { nameof(ProductTableModel.Pvp), new[] { "eq", "neq", "gt", "gte", "lt", "lte" } },
            { nameof(ProductTableModel.CategoryId), new[] { "eq", "neq" } },
            { nameof(ProductTableModel.CreatedAt), new[] { "eq", "neq", "gt", "gte", "lt", "lte" } }
        };

        var orderByFields = new[]
        {
            nameof(ProductTableModel.Id),
            nameof(ProductTableModel.Name),
            nameof(ProductTableModel.Pvp),
            nameof(ProductTableModel.CategoryId),
            nameof(ProductTableModel.CreatedAt)
        };

        return new SearchConfiguration
        {
            FilterFields = filterFields,
            OrderByFields = orderByFields
        };
    }

    public async Task<(IEnumerable<Product>, int)> SearchProducts
    (
        SearchCriteria criteria,
        bool withTotalResults = false,
        CancellationToken ct = default
    )
    {
        var query = EntityFrameworkCriteriaEngine
            .ApplyCriteria(_dbContext.Products.AsNoTracking(), criteria);

        var result = await query
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .ToListAsync(ct);

        if (!withTotalResults) return (result, 0);

        var totalQuery = EntityFrameworkCriteriaEngine
            .ApplyCriteria(_dbContext.Products.AsNoTracking(), criteria.WithoutPagination());

        var count = await totalQuery.CountAsync(ct);

        return (result, count);
    }

    public async Task<IEnumerable<Product>> GetAllProducts(CancellationToken ct = default)
    {
        var result = await _dbContext.Products
            .AsNoTracking()
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .ToListAsync(ct);

        return result;
    }

    public async Task<Product?> GetProductById(EntityId id, CancellationToken ct = default)
    {
        var result = await _dbContext.Products
            .AsNoTracking()
            .Where(table => table.Id == id.Value)
            .Select(table => Product.FromPrimitives(table.Id, table.Name, table.Pvp, table.CategoryId, table.CreatedAt))
            .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task CreateProduct(Product product, CancellationToken ct = default)
    {
        await CheckCategoryId(product.CategoryId, ct);

        var productTable = new ProductTableModel
        {
            Id = product.Id.Value,
            Name = product.Name.Value,
            Pvp = product.Pvp.Value,
            CategoryId = product.CategoryId?.Value,
            CreatedAt = product.CreatedAt
        };

        await _dbContext.Products.AddAsync(productTable, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task UpdateProduct(Product product, CancellationToken ct)
    {
        await CheckCategoryId(product.CategoryId, ct);

        var productTable = new ProductTableModel
        {
            Id = product.Id.Value,
            Name = product.Name.Value,
            Pvp = product.Pvp.Value,
            CategoryId = product.CategoryId?.Value,
            CreatedAt = product.CreatedAt
        };

        _dbContext.Update(productTable);
    }

    public Task DeleteProduct(EntityId id, CancellationToken ct)
    {
        var productTable = new ProductTableModel
        {
            Id = id.Value
        };

        _dbContext.Remove(productTable);

        return Task.CompletedTask;
    }

    public Task<bool> ExistsProduct(EntityId id, CancellationToken ct = default)
    {
        return _dbContext.Products.AsNoTracking().AnyAsync(x => x.Id == id.Value, ct);
    }

    private async Task CheckCategoryId(EntityId? categoryId, CancellationToken ct)
    {
        if
        (
            categoryId is not null
            && !await _dbContext.ProductCategories.AnyAsync(table => table.Id == categoryId.Value, ct)
        )
        {
            throw new RelationNotFoundException(nameof(Product), nameof(Product.CategoryId), categoryId!.Value.ToString());
        }
    }
}
