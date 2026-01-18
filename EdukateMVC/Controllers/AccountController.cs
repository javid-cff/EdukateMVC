using System.Threading.Tasks;
using EdukateMVC.Contexts;
using EdukateMVC.Models.Identity;
using EdukateMVC.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdukateMVC.Controllers
{
    public class AccountController(EdukateDbContext _context, SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            AppUser user = new()
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Fullname = vm.Fullname,
            };

            var result = await _userManager.CreateAsync(user, vm.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(vm);
            }

            await _userManager.AddToRoleAsync(user, "Member");

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.FindByEmailAsync(vm.Email);

            if (user is null)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(vm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemeber, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View(vm);
            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });

            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"
            });

            return Ok("Roles were created!");
        }
    }
}
