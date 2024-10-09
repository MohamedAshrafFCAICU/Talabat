using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
    public interface ISpecifications<TEntity , Tkey>
        where TEntity : BaseEntity<Tkey>
        where Tkey : IEquatable<Tkey>
    {
        public Expression<Func<TEntity , bool>>? Criteria { get; set; } // For Where LINQ Opreator Lamda Expression

        public List<Expression<Func<TEntity , Object>>> Includes { get; set; }
    }
}
