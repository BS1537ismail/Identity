using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles="Admin,CEO")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _rolemanager.Roles;
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var roles = _rolemanager.Roles;
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (!_rolemanager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
            {
                _rolemanager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
                return RedirectToAction("Index");
            }
            else ModelState.AddModelError("", "Role already exists");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _rolemanager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingRole = await _rolemanager.FindByIdAsync(id);
                if (existingRole == null)
                {
                    return NotFound();
                }

                existingRole.Name = role.Name;
                existingRole.NormalizedName = role.NormalizedName;

                var result = await _rolemanager.UpdateAsync(existingRole);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _rolemanager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _rolemanager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            var result = await _rolemanager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }
    }
}

