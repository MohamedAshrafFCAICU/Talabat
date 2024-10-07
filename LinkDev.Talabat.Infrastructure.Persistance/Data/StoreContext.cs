using LinkDev.Talabat.Core.Domain.Entities.Products;
using System.Reflection.Metadata.Ecma335;

namespace LinkDev.Talabat.Infrastructure.Persistance.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> Brands { get; set; }

        public DbSet<ProductCategory> Categories { get; set; }
    }
}
