using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using LinkDev.Talabat.Infrastructure.Persistance.Generic_Repository;

namespace LinkDev.Talabat.Infrastructure.Persistance.Repositories
{
    internal class GenericRepository<TEntity, Tkey>(StoreContext dbContext) : IGenericRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false)
        {
         

           return WithTracking ?
           await dbContext.Set<TEntity>().ToListAsync() :
           await dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Tkey Id)
        {
           

            return await dbContext.Set<TEntity>().FindAsync(Id);
                    
         }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec, bool WithTracking = false)
        {
            return await ApplySpecifications(spec).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await ApplySpecifications(spec).CountAsync();
        }

        public async Task AddAsync(TEntity entity) => await dbContext.Set<TEntity>().AddAsync(entity);
        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);        
        public void Delete(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);


        #region Helpers


        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, Tkey> spec)
        {
            return  SpecificationsEvaluator<TEntity, Tkey>.GetQuery(dbContext.Set<TEntity>(), spec);
        }

       

        #endregion


    }
}
