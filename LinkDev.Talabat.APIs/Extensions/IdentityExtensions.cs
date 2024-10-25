using LinkDev.Talabat.Core.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Core.Application.Services.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance._Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services) 
        {

            services.AddIdentity<ApplicationUser, IdentityRole>(idebtityOptions =>
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

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetService<IAuthService>();
            });

            return services;
        }
    }
}
