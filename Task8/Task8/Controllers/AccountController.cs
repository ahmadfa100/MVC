using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task8.Models;

namespace Task8.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserFromIdentity> _userManager;
        private readonly SignInManager<UserFromIdentity> _signInManager;

        public AccountController(UserManager<UserFromIdentity> userManager,
                             SignInManager<UserFromIdentity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            var newUser = new UserFromIdentity
            {
                FullName = register.FullName,
                UserName = register.FullName,
                Email = register.Email

            };
            var result = await _userManager.CreateAsync(newUser, register.Password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(newUser, "User");
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }

            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
                return View(register);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if(user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View(login);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
