using LinkDev.Talabat.Infrastructure.Persistance;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using LinkDev.Talabat.Infrastructure.Persistance.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            
            // Add services to the container.

            webApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationbuilder.Services.AddEndpointsApiExplorer();
            webApplicationbuilder.Services.AddSwaggerGen();


            webApplicationbuilder.Services.AddPersistanceServices(webApplicationbuilder.Configuration);
 
            
            #endregion

            var app = webApplicationbuilder.Build();

            #region Update-Database
          
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<StoreContext>(); // Inject StoreContext Explicitly
            var loggerFactory = services.GetRequiredService<ILoggerFactory>(); // Inject LoggerFactory Explicitly


            try
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(); // Update - Database
               
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Exception has been occured While Pending The Migrations or seeding Data");
            } 
           
            #endregion

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            
            app.Run();
        }
    }
}
