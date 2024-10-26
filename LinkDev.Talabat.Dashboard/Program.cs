using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance._Identity;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
         
            webApplicationBuilder.Services.AddControllersWithViews();

            #region Store Context
          
            webApplicationBuilder.Services.AddDbContext<StoreDbContext>((optionsBuilder) =>
               {
                   optionsBuilder
                   .UseLazyLoadingProxies()
                   .UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("StoreContext"));
               });

            #endregion

            #region Identity Context
           
            webApplicationBuilder.Services.AddDbContext<StoreIdentityDbContext>((optionsBuilder) =>
               {
                   optionsBuilder
                   .UseLazyLoadingProxies()
                   .UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("IdentityContext"));
               });

            webApplicationBuilder.Services.AddIdentity<ApplicationUser, IdentityRole>(idebtityOptions =>
            {
                idebtityOptions.User.RequireUniqueEmail = true;
                //idebtityOptions.User.AllowedUserNameCharacters = "asjdsbfcd151212&^%$#"


                idebtityOptions.SignIn.RequireConfirmedAccount = true;
                idebtityOptions.SignIn.RequireConfirmedEmail = true;
                idebtityOptions.SignIn.RequireConfirmedPhoneNumber = true;


                //idebtityOptions.Password.RequireNonAlphanumeric = true;
                //idebtityOptions.Password.RequiredUniqueChars = 2;
                //idebtityOptions.Password.RequiredLength = 6;
                //idebtityOptions.Password.RequireDigit = true;
                //idebtityOptions.Password.RequireLowercase = true;
                //idebtityOptions.Password.RequireUppercase = true;


                idebtityOptions.Lockout.AllowedForNewUsers = true;
                idebtityOptions.Lockout.MaxFailedAccessAttempts = 10;
                idebtityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);


                //idebtityOptions.Stores
                //idebtityOptions.ClaimsIdentity
                //idebtityOptions.Tokens

            })
           .AddEntityFrameworkStores<StoreIdentityDbContext>();

            #endregion



            #endregion

            var app = webApplicationBuilder.Build();


            #region Configure Kestrel Middlewares

            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion
            
            
            app.Run();
        }
    }
}
