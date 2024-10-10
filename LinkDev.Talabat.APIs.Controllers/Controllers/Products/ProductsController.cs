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

            return (Ok(products));
        }

        [HttpGet("{id:int}")]// GET: /api/Products
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is null)
                return NotFound(new { StatusCode = 404, Message = "NotFound" });

            return (Ok(product));
        }

        [HttpGet("brands")]// GET: /api/Products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands() 
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();

            return Ok(brands);
        }
        [HttpGet("categories")]// GET: /api/Products/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetCategoriesAsync();

            return Ok(categories);
        }

    }
}
