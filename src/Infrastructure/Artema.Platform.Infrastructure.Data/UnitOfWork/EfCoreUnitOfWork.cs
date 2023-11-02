using Artema.Platform.Application.Interfaces;
using Artema.Platform.Domain.Repositories;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Repositories;

namespace Artema.Platform.Infrastructure.Data.UnitOfWork;

public class EfCoreUnitOfWork : IUnitOfWork
{
    private readonly ArtemaPlatformDbContext _dbContext;
    private IProductRepository _productRepository = default!;

    public IProductRepository ProductRepository { get => _productRepository ??= new ProductRepository(_dbContext); }

    public EfCoreUnitOfWork(ArtemaPlatformDbContext dbContext)
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
        _dbContext.Dispose();
    }
}
