using AutoMapper;
using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Helpers;
using LinkDev.Talabat.Dashboard.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork , IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product,  int>().GetAllAsync();

            #region Mapping
            var mappedProducts = products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                PictureUrl = p.PictureUrl,
                Category = p.Category!,
                Brand = p.Brand!,

            }).ToList();     
            #endregion

            return View(mappedProducts);
        }

        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> create(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                else
                    model.PictureUrl = "images/products/double-caramel-frappuccino.png";

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(model);

                mappedProduct.CreatedBy = "1";
                mappedProduct.LastModifiedBy = "1";
                mappedProduct.NormalizedName = mappedProduct.Name.ToUpper();

                await _unitOfWork.GetRepository<Product ,  int>().AddAsync(mappedProduct); 
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model); 
        }
    
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<Product , ProductViewModel>(products!);
         
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id , ProductViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();  
            }
            if(ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    PictureSettings.DeleteFile("products", model.PictureUrl!);
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                else
                    model.PictureUrl = PictureSettings.UploadFile(model.Image!, "products");

                var mappedProducts = _mapper.Map<ProductViewModel, Product>(model);
                _unitOfWork.GetRepository<Product, int>().Update(mappedProducts);
                var result = await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(products!);

            return View(mappedProduct);
        }

        [HttpPost]
		public async Task<IActionResult> Delete(int id , ProductViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound();  
            }
            try
            {
                var product = await _unitOfWork.GetRepository<Product , int>().GetAsync(id);
            
                if(product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile("products", product.PictureUrl);
                }
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            
            catch (Exception)
            {

                return View(model); 
            }
        }

	}
}
