using LinkDev.Talabat.Core.Domain.Contracts.Persistance;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
	public class BrandController(IUnitOfWork _unitOfWork) : Controller
	{
		public async Task<IActionResult> Index()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync();	
			return View(brands);
		}

		public async Task<IActionResult> Create(ProductBrand model)
		{
			try
			{
				await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(model);
				await _unitOfWork.CompleteAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ModelState.AddModelError("Name", "Please Enter New Name");
				return View("Index" ,await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
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
