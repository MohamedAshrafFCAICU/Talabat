using LinkDev.Talabat.Core.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Specifications.Products
{
    // This Class Responsible For Make The Expressions Needed To Get All Products  
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort , int? brandId , int? categoryId , int pageSize , int pageIndex , string? search) 
            : base(
                    

                  P => 
                        (string.IsNullOrEmpty(search) || P.NormalizedName.Contains(search))
                                &&
                        (!brandId.HasValue || P.BrandId == brandId.Value)
                                &&
                        (!categoryId.HasValue || P.CategoryId == categoryId.Value)    

                  )
        {
            AddIncludes();

            //OrderBy(P => P.Name);
          
            switch (sort)
            {
                case "nameDesc":
                    //OrderByDesc(P => P.Name);
                    AddOrderByDesc(P => P.Name);
                    break;
                case "priceAsc":
                    //OrderBy = P => P.Price;
                    AddOrderBy(P => P.Price);
                    break;
                case "priceDesc":
                    //OrderByDesc(P => P.Price);
                    AddOrderByDesc(P => P.Price);
                    break;
                default:
                    //OrderByc(P => P.Name);
                    AddOrderBy(P => P.Name);
                    break;
            }


            // Ex: 
            // Total Products = 18 ~ 20 [Rounded Up To be dividable By 5]
            // Page Size = 5
            // So , The Number Of Pages  =4
            // Page Index = 3
            ApplyPagination(pageSize * (pageIndex - 1), pageSize); // (Skip , Take)
        }

        private protected override  void AddIncludes()
        {
            base.AddIncludes();

            Includes.Add(P => P.Brand!);
            Includes.Add(P => P.Category!);
        }

        // This Class Responsible For Make The Expressions Needed To Get a specific Product
        public ProductWithBrandAndCategorySpecifications(int id) : base(id)
        {
            AddIncludes();
        }

       

    }
}
