using LinkDev.Talabat.Core.Domain.Contracts.Persistance.DbInitializer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.Common
{
    public abstract class DbInitializer(DbContext dbContext) : IDbInitializer
    {
        public virtual async Task InitializeAsync()
        {
            var pendingMigrations = dbContext.Database.GetPendingMigrations();
            if (pendingMigrations.Any())
                await dbContext.Database.MigrateAsync(); // Update - Database
        }

        public abstract Task SeedAsync();
        
    }
}
