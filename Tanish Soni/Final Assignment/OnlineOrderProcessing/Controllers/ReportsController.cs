using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Services;

namespace OnlineOrderProcessing.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            DateTime? fromDate,
            DateTime? toDate,
            Guid? productId,
            OrderStatus? status)
        {
            var model = await _reportService.GetSalesReport(
                fromDate,
                toDate,
                productId,
                status);

            return View(model);
        }
    }
}
