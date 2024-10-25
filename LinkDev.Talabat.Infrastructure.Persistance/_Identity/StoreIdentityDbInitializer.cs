using LinkDev.Talabat.Core.Domain.Contracts.Persistance.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance.Common;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistance._Identity
{
    internal class StoreIdentityDbInitializer(StoreIdentityDbContext dbContext , UserManager<ApplicationUser> userManager) 
        : DbInitializer(dbContext), IStoreIdentityDbInitializer
    {
     
        public override async Task SeedAsync()
        {
          
            var user = new ApplicationUser()
            {
                DisplayName = "Ahmed Nasr",
                UserName = "ahmed.nasr",
                Email = "ahmed.nasr@linkdev.com",
                PhoneNumber = "01122334455",
            }; 

            await userManager.CreateAsync(user , "P@ssw0rd");    
            

        }
    }
}
