using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.Models.Roles;
using LinkDev.Talabat.Dashboard.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
	public class UserController(RoleManager<IdentityRole> _roleManager , UserManager<ApplicationUser> _userManager) : Controller
	{
		public async Task<IActionResult> Index() // GET:
		{
			var applicationUsers = await _userManager.Users.ToListAsync();

			var users = new List<ReturnedUserViewModel>();
			foreach (var user in applicationUsers)
			{
				var userViewModel = new ReturnedUserViewModel
				{
					Id = user.Id,
					DisplayName = user.DisplayName,
					UserName = user.UserName!,
					Email = user.Email!,
					PhoneNumber = user.PhoneNumber!,
					Roles = await _userManager.GetRolesAsync(user) 
				};
				users.Add(userViewModel);
			}

			return View(users);
		}

		public async Task<IActionResult> Edit(string? id) // GET:
		{
			if(id is not null)
			{
				var user = await _userManager.FindByIdAsync(id);

				if (user is { })
				{
					var allRoles = await _roleManager.Roles.ToListAsync();
					var viewModel = new EditUserViewModel()
					{
						Id = user.Id,
						UserName = user.UserName!,
						Roles = allRoles.Select(r => new EditRoleViewMoel()
						{
							Id = r.Id,
							Name = r.Name!,
							IsSelected = _userManager.IsInRoleAsync(user, r.Name!).Result
						}).ToList()

					};

					return View(viewModel);	
				}
	
			}

			return View(nameof(Index));
		} 

		[HttpPost]
		public async Task<IActionResult> Edit(string? id , EditUserViewModel model) // POST:
		{
			if(id is not null)
			{
				var user = await _userManager.FindByIdAsync(model.Id);

				if(user is { })
				{
					var userRoles = await _userManager.GetRolesAsync(user);
					foreach (var role in model.Roles)
					{
						if (userRoles.Any(r => r == role.Name) && !role.IsSelected)
							await _userManager.RemoveFromRoleAsync(user, role.Name);

						if (!userRoles.Any(r => r == role.Name) && role.IsSelected)
							await _userManager.AddToRoleAsync(user, role.Name);
					}
				}
			}
			return RedirectToAction(nameof(Index));	
		}

	}
}
