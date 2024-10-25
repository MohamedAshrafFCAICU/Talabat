using LinkDev.Talabat.Core.Domain.Contracts.Persistance.DbInitializer;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtension
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var storeContextInitializer = services.GetRequiredService<IStoreDbInitializer>();
            var identityContextInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); 


            try
            {

                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync();


                await identityContextInitializer.InitializeAsync();
                await identityContextInitializer.SeedAsync();

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Exception has been occured While Pending The Migrations or seeding Data");
            }
           
            return app; 
        }
    }
}
