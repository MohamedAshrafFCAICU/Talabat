using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]// GET:  /api/Products
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetProductsAsync(specParams);

            return (Ok(products));
        }

        [HttpGet("{id:int}")]// GET: /api/Products
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductAsync(id);

            if (product is null)
                return NotFound(new ApiResponse(404 , $"The Product With Id:{id} is not found"));

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
