using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
    // This Class Responsible For Make The Expressions Needed To Get All Products  
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product , int>
    {
        public ProductWithBrandAndCategorySpecifications() : base() 
        {
            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }
    }
}
