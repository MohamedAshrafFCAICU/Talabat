using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistance.Repositories.Generic_Repository
{
    internal static class SpecificationsEvaluator<TEntity , Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>  
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> spec)
        {
            var query = inputQuery; // _dbContext.Set<TEntity>()

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            else if(spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);

            // query = _dbContext.Set<Product>.Where(P => P.Id.Equals(id))

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            // query = _dbContext.Set<Product>
            //                   .Where(P => P.Id.Equals(id))
            //                   .Include(P => P.Brands)
            //                   .Include(P => P.Categories)

            return query;   
            
        }
    }
}