using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly SchooErpContext context;

        public DashboardController(SchooErpContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
