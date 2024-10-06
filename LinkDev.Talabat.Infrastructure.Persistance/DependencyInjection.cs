using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreContext>((optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("StoreContext"));
            });

            Services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();

            return Services;
        }
    }
}
