using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task8.Models;

namespace Task8.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<UserFromIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<UserFromIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> isSuperAdmin()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return false;

            return await _userManager.IsInRoleAsync(user, "SuperAdmin");
        }

        public async Task<IActionResult> Index()
        {
            if (!await isSuperAdmin())
                return Forbid();

            var users = _userManager.Users.ToList();

            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!await isSuperAdmin())
                return Forbid();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            ViewBag.CurrentRole = roles.FirstOrDefault();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string roleName)
        {
            if (!await isSuperAdmin())
                return Forbid();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            if (roleName != "User" &&
                roleName != "Admin" &&
                roleName != "SuperAdmin")
                return BadRequest();

            if (!await _roleManager.RoleExistsAsync(roleName))
                return BadRequest();

            if (id == _userManager.GetUserId(User) &&
                roleName != "SuperAdmin")
                return Forbid();

            var oldRoles = await _userManager.GetRolesAsync(user);

            if (!oldRoles.Contains(roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (!result.Succeeded)
                    return BadRequest();
            }

            var rolesToRemove = oldRoles
                .Where(r => r != roleName)
                .ToList();

            if (rolesToRemove.Count > 0)
            {
                var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                if (!result.Succeeded)
                    return BadRequest();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (!await isSuperAdmin())
                return Forbid();

            if (id == _userManager.GetUserId(User))
                return Forbid();

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                return BadRequest();

            return RedirectToAction("Index");
        }
    }
}