using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
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

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
            if (typeof(TEntity) == typeof(Product))
                return WithTracking ?
                    (IEnumerable<TEntity>)await dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
                    (IEnumerable<TEntity>)await dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();


           return WithTracking ?
           await dbContext.Set<TEntity>().ToListAsync() :
           await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }
      
        public async Task<TEntity?> GetAsync(Tkey Id)
        {
            if (typeof(TEntity) == typeof(Product))
                return await dbContext.Set<Product>().Where(P => P.Id.Equals(Id)).Include(P => P.Brand).Include(P => P.Category).FirstOrDefaultAsync() as TEntity;

            return await dbContext.Set<TEntity>().FindAsync(Id);
                    
         }  
        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);        
        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);  
     
    }
}
