using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistance.Data;
using LinkDev.Talabat.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.UnitOfWork
{
    internal class UnitOfwork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public UnitOfwork(StoreDbContext dbContxt)
        {
           _dbContext = dbContxt;
            _repositories = new ();
        }
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
          where TEntity : BaseEntity<Tkey>
          where Tkey : IEquatable<Tkey>
        {
            return (IGenericRepository<TEntity, Tkey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, Tkey>(_dbContext));
        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();  
      

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

      
    }
}
