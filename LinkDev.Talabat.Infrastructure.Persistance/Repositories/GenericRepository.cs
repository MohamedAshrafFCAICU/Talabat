using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreContext dbContext) : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false) =>  WithTracking ? await dbContext.Set<TEntity>().ToListAsync() : await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        public async Task<TEntity?> GetAsync(Tkey Id) => await dbContext.Set<TEntity>().FindAsync(Id);
        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);        
        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);  
     
    }
}
