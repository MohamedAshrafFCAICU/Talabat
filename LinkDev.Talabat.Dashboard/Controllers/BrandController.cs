using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Dashboard.Models.Brands;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
	public class BrandController(IUnitOfWork _unitOfWork) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync();

			var mappedBrands = brands.Select(b => new BrandViewModel()
			{
				Id = b.Id,
				Name = b.Name,
			}).ToList();

			return View(mappedBrands);
		}

		public async Task<IActionResult> Create(BrandViewModel model)
		{
			try
			{
				var mappedBrand = new ProductBrand()
				{
					Id = model.Id,
					Name = model.Name,
					CreatedBy = "1",
					LastModifiedBy = "1",

				};
				await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(mappedBrand);
				await _unitOfWork.CompleteAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError("Name", "Please Enter New Name");
				return RedirectToAction(nameof(Index));
			}
		}

		public async Task<IActionResult> Delete(int id)
		{
			var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);
			_unitOfWork.GetRepository<ProductBrand , int>().Delete(brand!);
			await _unitOfWork.CompleteAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
