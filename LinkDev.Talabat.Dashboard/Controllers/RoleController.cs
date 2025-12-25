using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Dashboard.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Dashboard.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager) : Controller
    {
        
        public async Task<IActionResult> Index() // GET: 
        {
            var roles = await _roleManager.Roles.ToListAsync();

      
            return View(roles);
        }
      
        [HttpPost]
        public async Task<IActionResult> Create(CreatedRoleViewModel model)// POST:
		{
            if(ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name);
                if (!roleExist)
                {
                    var role = new IdentityRole { Name = model.Name };
                    await _roleManager.CreateAsync(role);
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Name), "Name is Already Existed");
                    return View(nameof(Index), await _roleManager.Roles.ToListAsync());
                }
            }
			return RedirectToAction(nameof(Index));

		} 

        public async Task<IActionResult> Delete(string? id)// POST:
		{
            if(id is not null)
            {
				var role = await _roleManager.FindByIdAsync(id);
                if(role is { })
				    await _roleManager.DeleteAsync(role!);
			}
            return RedirectToAction(nameof(Index)); 

        } 

        public async Task<IActionResult> Edit(string? id) // GET: 
        {
            if(id is not null)
            {
                var role = await _roleManager.FindByIdAsync(id);

                if(role is { })
                {
                    var mappedRole = new EditRoleViewMoel()
                    {
                        Name = role.Name!
                    };

                    return View(mappedRole);
                }
            }

            return View(nameof(Index)); 
        }

        [HttpPost]
		public async Task<IActionResult> Edit(string? id , EditRoleViewMoel model) // POST: 
        {
			if (ModelState.IsValid)
			{
				var roleExist = await _roleManager.RoleExistsAsync(model.Name);
				if (!roleExist)
				{
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    await _roleManager.UpdateAsync(role!);   
				}
				else
				{
					ModelState.AddModelError(nameof(model.Name), "Name is Already Existed");
					return View(nameof(Index), await _roleManager.Roles.ToListAsync());
				}
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
