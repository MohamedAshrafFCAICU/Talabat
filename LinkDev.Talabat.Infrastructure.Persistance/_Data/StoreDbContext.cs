using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistance._Common;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace LinkDev.Talabat.Infrastructure.Persistance.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                 type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreDbContext));
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }
    }
}
