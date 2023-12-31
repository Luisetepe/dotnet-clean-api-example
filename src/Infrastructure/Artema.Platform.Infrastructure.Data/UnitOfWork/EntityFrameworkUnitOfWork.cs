using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Repositories;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Repositories.EntityFramework;

namespace Artema.Platform.Infrastructure.Data.UnitOfWork;

public class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly ArtemaPlatformDbContext _dbContext;
    private bool _disposed;
    private IProductRepository? _productRepository;
    private IProductCategoryRepository? _productCategoryRepository;

    public IProductRepository ProductRepository => _productRepository ??= new ProductEfRepository(_dbContext);
    public IProductCategoryRepository ProductCategoryRepository => _productCategoryRepository ??= new ProductCategoryEfRepository(_dbContext);

    public EntityFrameworkUnitOfWork(ArtemaPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveAsync(CancellationToken ct = default)
    {
        await _dbContext.SaveChangesAsync(ct);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            _dbContext.Dispose();
        }

        _disposed = true;
    }
}
