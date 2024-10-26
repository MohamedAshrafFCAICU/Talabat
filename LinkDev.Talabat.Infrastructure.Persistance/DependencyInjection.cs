using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance._Identity;
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
            #region Store Context
         
            Services.AddDbContext<StoreDbContext>((optionsBuilder) =>
               {
                   optionsBuilder
                   .UseLazyLoadingProxies()
                   .UseSqlServer(Configuration.GetConnectionString("StoreContext"));
               });

            Services.AddScoped<IStoreDbInitializer, StoreDbInitializer>();
            Services.AddScoped(typeof(IStoreDbInitializer), typeof(StoreDbInitializer));

            Services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptors));

            #endregion

            #region Identity Context

            Services.AddDbContext<StoreIdentityDbContext>((optionsBuilder) =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(Configuration.GetConnectionString("IdentityContext"));
            });

            Services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

            #endregion

            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfwork));


          

            return Services;
        }
    }
}
