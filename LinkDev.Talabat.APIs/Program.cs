using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistance;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            
            // Add services to the container.

            webApplicationbuilder.Services.AddControllers()
                .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationbuilder.Services.AddEndpointsApiExplorer();
            webApplicationbuilder.Services.AddSwaggerGen();

            webApplicationbuilder.Services.AddHttpContextAccessor();    

            webApplicationbuilder.Services.AddScoped<ILoggedInUserService , LoggedInUserService>();    



            webApplicationbuilder.Services.AddPersistanceServices(webApplicationbuilder.Configuration);

            webApplicationbuilder.Services.AddApplicationServices();

            #endregion

            var app = webApplicationbuilder.Build();

            #region Databases Initialization
          
            await app.InitializeStoreContextAsync();
           
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
