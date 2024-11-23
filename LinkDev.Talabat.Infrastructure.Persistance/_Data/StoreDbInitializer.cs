using LinkDev.Talabat.Core.Domain.Contracts.Persistance.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistance.Common;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Persistance.Data
{
    public class StoreDbInitializer(StoreDbContext dbContext) : DbInitializer(dbContext), IStoreDbInitializer
    {
        public override async Task SeedAsync()
        {
            if (!dbContext.Brands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/brands.json"); // Data returned Formated in Json
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    await dbContext.Set<ProductBrand>().AddRangeAsync(Brands);
                    await dbContext.SaveChangesAsync();
                }

            }
            if (!dbContext.Categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/categories.json"); // Data returned Formated in Json
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await dbContext.SaveChangesAsync();
                }

            }
            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/products.json"); // Data returned Formated in Json
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    await dbContext.Set<Product>().AddRangeAsync(Products);
                    await dbContext.SaveChangesAsync();
                }

            }
            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/_Data/Seeds/delivery.json"); // Data returned Formated in Json
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count > 0)
                {
                    await dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                    await dbContext.SaveChangesAsync();
                }

            }
        }
    }
}
