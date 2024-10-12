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

        public List<Expression<Func<TEntity , object>>> Includes { get; set; } // For Include LINQ Opreator Lamda Expressions

        public Expression<Func<TEntity , object>>? OrderBy { get; set; } // For OrderBy LINQ Opreator Lamda Expression
        public Expression<Func<TEntity , object>>? OrderByDesc { get; set; } // For OrderByDescending LINQ Opreator Lamda Expression
    }
}
