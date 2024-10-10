using LinkDev.Talabat.Core.Domain.Contracts.Persistance;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtension
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<IStoreContextInitializer>(); // Inject StoreContext Explicitly
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // Inject LoggerFactory Explicitly


            try
            {

                await dbContext.InitializeAsync();
                await dbContext.SeedAsync();

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
