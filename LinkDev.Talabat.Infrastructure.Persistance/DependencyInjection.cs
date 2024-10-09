using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using LinkDev.Talabat.Infrastructure.Persistance.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            Services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptors));

            return Services;
        }
    }
}
