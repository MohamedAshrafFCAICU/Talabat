using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]// GET:  /api/Products
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetProductsAsync();

            return(Ok(products));   
        }




    }
}
