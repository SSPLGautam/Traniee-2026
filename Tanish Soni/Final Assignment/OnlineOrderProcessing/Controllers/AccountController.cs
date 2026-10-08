using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.ViewModels;
using System.Security.Claims;

namespace OnlineOrderProcessing.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthServices _authServices;

        public AccountController(IAuthServices authServices)
        {
            _authServices = authServices;
        }
        public IActionResult Index()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var role = User.FindFirstValue(ClaimTypes.Role);

            ViewBag.Email = email;
            ViewBag.Role = role;


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Data!");
                return View(model);
            }
            
            var result = await _authServices.Login(model);
            if (result.IsFailure)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("Index", "Products");
        }


        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid Data!");
                return View(model);
            }

            var result = await _authServices.Register(model);
            if (result.IsFailure)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(model);
            }

            return RedirectToAction("Index", "Products");
        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authServices.Logout();

            return RedirectToAction("Login", "Account");
        }
    }
}
