using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistance._Common;
using LinkDev.Talabat.Infrastructure.Persistance._Identity.Config;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance._Identity
{
    internal class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
            : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,
                type => type.GetCustomAttribute<DbContextTypeAttribute>()?.DbContextType == typeof(StoreIdentityDbContext));
        }


        
    }
}
