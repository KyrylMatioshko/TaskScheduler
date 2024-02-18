using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskSched.Data.Models;
using TaskSched.ViewModels;

namespace TaskSched.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View(new UserRegistrationViewModel());
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(UserRegistrationViewModel userRegistration)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userRegistration.Email ?? "");

                if (user == null)
                {
                    User newUser = new User()
                    {
                        FirstName = userRegistration.FirstName,
                        LastName = userRegistration.LastName,
                        Email = userRegistration.Email,
                        UserName = userRegistration.Email
                    };

                    IdentityResult result = await _userManager.CreateAsync(newUser, userRegistration.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false);

                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(UserRegistrationViewModel.Email), "Користувач з таким Email вже існує");
                }

            }
            return View(userRegistration);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel()
            {
                Email = "Shevchenko@gmail.com",
                Password = "0123456789",
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, userLogin.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userLogin);
        }


        //[HttpGet]
        //public IActionResult LogoutGet()
        //{
        //    return View("Logout",new LogoutViewModel());
        //}

        //[HttpPost]
        //public async Task<IActionResult> LogoutPost(LogoutViewModel logoutViewModel)
        //{
        //    if (logoutViewModel.IsConfirmed)
        //    {
        //        await _signInManager.SignOutAsync();
        //        return Json( new { Success = true });
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

		[HttpGet]
		[Route("Logout")]
		public async Task<IActionResult> Logout(LogoutViewModel logoutViewModel)
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
