using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Specifications.Products
{
    public class ProductSpecParams
    {

        private const int maxPageSize = 10;
        private int pageSize = 5;
       
        private string? search;

        public string? Sort { get; set; }

        public int? BrandId { get; set; }

        public int? CategoryId { get; set; }

       
        public string? Search 
        {     get { return search; }
              set { search = value?.ToUpper(); }
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
    }
}
