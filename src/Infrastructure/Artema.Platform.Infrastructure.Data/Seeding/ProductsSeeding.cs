using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.TableModels;
using Microsoft.EntityFrameworkCore;

namespace Artema.Platform.Infrastructure.Data.Seeding;

public class ProductsSeeding : IDatabaseSeeder
{
    public async Task SeedTestDataAsync(ArtemaPlatformDbContext dbContext)
    {
        // Create ten categories
        var categories = new List<ProductCategoryTable>
        {
            new() { Id = Guid.NewGuid(), Name = "Bebidas" },
            new() { Id = Guid.NewGuid(), Name = "Snacks" },
            new() { Id = Guid.NewGuid(), Name = "Streaming" },

        };
        
        var products = new List<ProductTable>
        {
            new() { Id = Guid.NewGuid(), Name = "CocaCola", Pvp = 2500, CategoryId = categories[0].Id },
            new() { Id = Guid.NewGuid(), Name = "Fanta Naranja", Pvp = 1500, CategoryId = categories[0].Id },
            new() { Id = Guid.NewGuid(), Name = "Fanta Limon", Pvp = 1500, CategoryId = categories[0].Id },
            new() { Id = Guid.NewGuid(), Name = "Aquarius", Pvp = 3000, CategoryId = categories[0].Id },
            new() { Id = Guid.NewGuid(), Name = "Monster", Pvp = 3500, CategoryId = categories[0].Id },
            new() { Id = Guid.NewGuid(), Name = "Doritos", Pvp = 1200, CategoryId = categories[1].Id },
            new() { Id = Guid.NewGuid(), Name = "Pelotazos", Pvp = 1700, CategoryId = categories[1].Id },
            new() { Id = Guid.NewGuid(), Name = "Fantasmitas", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = Guid.NewGuid(), Name = "Fritos", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = Guid.NewGuid(), Name = "Gublins", Pvp = 1350, CategoryId = categories[1].Id },
            new() { Id = Guid.NewGuid(), Name = "Netflix", Pvp = 4500, CategoryId = categories[2].Id },
            new() { Id = Guid.NewGuid(), Name = "HBO Max", Pvp = 4500, CategoryId = categories[2].Id },
            new() { Id = Guid.NewGuid(), Name = "Amazon Prime", Pvp = 4500, CategoryId = categories[2].Id },
        };

        if(!await dbContext.ProductCategories.AnyAsync())
            await dbContext.ProductCategories.AddRangeAsync(categories);
        if(!await dbContext.Products.AnyAsync())
            await dbContext.Products.AddRangeAsync(products);
    }
}