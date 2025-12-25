using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts.Persistance
{
    public interface IGenericRepository<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
     
        public  Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, Tkey> spec, bool WithTracking = false);

        Task<TEntity?> GetAsync(Tkey Id);
        Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity, Tkey> spec);

        Task<int> GetCountAsync(ISpecifications<TEntity, Tkey> spec);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}
