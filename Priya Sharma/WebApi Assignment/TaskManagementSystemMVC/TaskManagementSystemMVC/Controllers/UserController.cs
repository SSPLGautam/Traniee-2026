using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystemMVC.Services;

namespace TaskManagementSystemMVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name != "admin@admin.com")
            {
                return Forbid();
            }

            var users = await _userService.GetAllUsersAsync();
            users = users.Where(u => u.Email != "admin@admin.com").ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoles(string userId, List<string> selectedRoles)
        {
            if (User.Identity.Name != "admin@admin.com")
            {
                return Forbid();
            }
            await _userService.UpdateRolesAsync(userId, selectedRoles);

            return RedirectToAction(nameof(Index));
        }
    }
}
