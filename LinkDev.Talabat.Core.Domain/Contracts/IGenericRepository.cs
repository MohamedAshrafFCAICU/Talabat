using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface IGenericRepository<TEntity , Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool WithTracking = false);
        
        Task<TEntity?> GetAsync(Tkey Id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);    
    }
}
