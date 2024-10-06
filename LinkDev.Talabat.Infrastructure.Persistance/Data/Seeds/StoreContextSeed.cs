using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.Data.Seeds
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {

            if (!dbContext.Brands.Any())
            {
                var BrandsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/brands.json"); // Data returned Formated in Json
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                
                if (Brands?.Count > 0)
                {
                    await dbContext.Set<ProductBrand>().AddRangeAsync(Brands);
                    await dbContext.SaveChangesAsync();
                }

            }
            if (!dbContext.Categories.Any())
            {
                var CategoriesData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/categories.json"); // Data returned Formated in Json
                var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);

                if (Categories?.Count > 0)
                {
                    await dbContext.Set<ProductCategory>().AddRangeAsync(Categories);
                    await dbContext.SaveChangesAsync();
                }

            }
            if (!dbContext.Products.Any())
            {
                var ProductsData = await File.ReadAllTextAsync("../LinkDev.Talabat.Infrastructure.Persistance/Data/Seeds/products.json"); // Data returned Formated in Json
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    await dbContext.Set<Product>().AddRangeAsync(Products);
                    await dbContext.SaveChangesAsync();
                }

            }


        }
    }
}
