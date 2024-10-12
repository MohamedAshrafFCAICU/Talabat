using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey>
         where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public Expression<Func<TEntity , bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get ; set; } = null ;
        public Expression<Func<TEntity, object>>? OrderByDesc { get; set; } = null;

        protected BaseSpecifications()
        {
            
        }

        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression) // Consttructor Will Used To  Get All records => As I Make Criteria , Includes refer to NULL
        {
            Criteria = CriteriaExpression;

        }

        protected BaseSpecifications(Tkey id) // Constructor Used To Filter Based On the Id 
        {
            Criteria = E => E.Id.Equals(id);
        }

        private protected virtual void AddIncludes()
        {
          
        }

        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;    
        }


        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> OrderByDescExpression)
        {
            OrderByDesc = OrderByDescExpression;
        }
    }
}
