using LinkDev.Talabat.Core.Application.Abstraction.Models.Auth;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.Dashboard.Controllers
{
	public class AdminController(SignInManager<ApplicationUser> _signinManager, UserManager<ApplicationUser> _userManager) : Controller
	{
		public IActionResult Login()
		{
			return View();
		} // GET: 

		[HttpPost]
		public async Task<IActionResult> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);

			if(user is null)
			{
				ModelState.AddModelError(nameof(model.Email), "Invalid Email");
				return RedirectToAction(nameof(Login));	
			}

			var result = await _signinManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
			{
				ModelState.AddModelError(string.Empty, "You are not authorized");
				return RedirectToAction(nameof(model));
			}
			else
				return RedirectToAction("Index", "Home");
		} // POST: 

		public async Task<IActionResult> Logout()
		{
			await _signinManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
			
		} // GET: 
	}
}
